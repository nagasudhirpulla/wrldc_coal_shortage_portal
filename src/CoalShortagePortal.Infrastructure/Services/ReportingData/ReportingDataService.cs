﻿using System;
using System.Collections.Generic;
using System.Globalization;
using CoalShortagePortal.Core.ReportingData;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Oracle.ManagedDataAccess.Client;

namespace CoalShortagePortal.Infrastructure.Services.ReportingData
{
    public class ReportingDataService
    {
        private readonly string _reportingConnStr;
        private readonly ILogger<ReportingDataService> _logger;
        public ReportingDataService(IConfiguration configuration, ILogger<ReportingDataService> logger)
        {
            _reportingConnStr = configuration["ConnectionStrings:ReportingConnection"];
            _logger = logger;
        }

        public List<UnRevGeneratorOutage> GetLatestUnrevivedGenOutages(DateTime targetDt)
        {
            List<UnRevGeneratorOutage> unrevOutages = new();

            using OracleConnection con = new(_reportingConnStr);

            using OracleCommand cmd = con.CreateCommand();
            con.Open();
            cmd.CommandText = @"SELECT
    rto.id,
    rto.element_id,
    rto.shut_down_type_id,
    rto.shut_down_type_name,
    rto.elementname,
    rto.INSTALLED_CAPACITY,
    trunc(rto.outage_date) AS outage_date,
    rto.outage_time,
    trunc(rto.expected_date) as expected_date,
    rto.expected_time,
    rto.reason,
    rto.owners,
    rto.shutdown_tag,
    rto.shutdown_tag_id
FROM
         (
        SELECT
            outages.id,
            outages.entity_id,
            outages.element_id,
            outages.elementname,
            outages.shutdown_tag_id,
            outages.outage_remarks,
            outages.revived_date,
            outages.revived_time,
            outages.outage_time,
            outages.outage_date,
            outages.expected_date,
            outages.expected_time,
            ent_master.entity_name,
            owner_details.owners,
            gu.INSTALLED_CAPACITY, 
            reas.reason,
            sd_type.id             AS shut_down_type_id,
            sd_type.name           AS shut_down_type_name,
            sd_tag.name            AS shutdown_tag,
            to_char(outages.outage_date, 'YYYY-MM-DD')
            || ' '
            || outages.outage_time AS out_date_time
        FROM
            reporting_web_ui_uat.real_time_outage     outages
            LEFT JOIN reporting_web_ui_uat.outage_reason        reas ON reas.id = outages.reason_id
            LEFT JOIN reporting_web_ui_uat.entity_master        ent_master ON ent_master.id = outages.entity_id
            LEFT JOIN reporting_web_ui_uat.shutdown_outage_tag  sd_tag ON sd_tag.id = outages.shutdown_tag_id
            LEFT JOIN reporting_web_ui_uat.shutdown_outage_type sd_type ON sd_type.id = outages.shut_down_type
            LEFT JOIN reporting_web_ui_uat.GENERATING_UNIT gu ON gu.ID = outages.ELEMENT_ID 
            LEFT JOIN (SELECT
                            LISTAGG(own.owner_name, ',') WITHIN GROUP(
                            ORDER BY
                                owner_name
                            ) AS owners,
                            parent_entity_attribute_id AS element_id
                        FROM
                            reporting_web_ui_uat.entity_entity_reln ent_reln
                            LEFT JOIN reporting_web_ui_uat.owner own ON own.id = ent_reln.child_entity_attribute_id
                        WHERE
                                ent_reln.child_entity = 'Owner'
                            AND ent_reln.parent_entity = 'GENERATING_STATION'
                            AND ent_reln.child_entity_attribute = 'OwnerId'
                            AND ent_reln.parent_entity_attribute = 'Owner'
                        GROUP BY
                            parent_entity_attribute_id
                    ) owner_details ON owner_details.element_id = gu.fk_generating_station
    ) rto
    INNER JOIN (
        SELECT
            element_id,
            entity_id,
            MAX(to_char(outage_date, 'YYYY-MM-DD')
                || ' '
                || outage_time) AS out_date_time
        FROM
            reporting_web_ui_uat.real_time_outage
        where outage_date<=:targetDt 
        GROUP BY
            entity_id,
            element_id
    ) latest_out_info ON ( ( latest_out_info.entity_id = rto.entity_id )
                           AND ( latest_out_info.element_id = rto.element_id )
                           AND ( latest_out_info.out_date_time = rto.out_date_time ) )
WHERE rto.entity_name='GENERATING_UNIT' AND rto.revived_time IS NULL
ORDER BY
    rto.out_date_time DESC
";
            cmd.Parameters.Add(new OracleParameter("targetDt", new DateTime(targetDt.Year, targetDt.Month, targetDt.Day, 23, 59, 59)));
            OracleDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                UnRevGeneratorOutage outage = new();
                outage.RTOutageId = DbUtils.SafeGetInt(reader, "ID");

                outage.ElementId = DbUtils.SafeGetInt(reader, "ELEMENT_ID");
                outage.ElementName = DbUtils.SafeGetString(reader, "ELEMENTNAME");


                outage.OutageTypeId = DbUtils.SafeGetInt(reader, "SHUT_DOWN_TYPE_ID");
                outage.OutageType = DbUtils.SafeGetString(reader, "SHUT_DOWN_TYPE_NAME");

                // derive outage DateTime
                DateTime? outageDate = DbUtils.SafeGetDt(reader, "OUTAGE_DATE");
                string outageTimeStr = DbUtils.SafeGetString(reader, "OUTAGE_TIME");
                bool isOutageTimeStrValid = (outageDate != null) && (!string.IsNullOrWhiteSpace(outageTimeStr)) && (outageTimeStr.Length >= 5);
                if (!isOutageTimeStrValid)
                {
                    continue;
                }
                try
                {
                    DateTime outageDt = DateTime.ParseExact($"{outageDate?.ToString("yyyy-MM-dd")} {outageTimeStr[..5]}", "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture);
                    outage.OutageDateTime = outageDt;
                }
                catch (FormatException)
                {
                    continue;
                }

                // derive expected revival DateTime
                DateTime? expectedDate = DbUtils.SafeGetDt(reader, "EXPECTED_DATE");
                string expectedTimeStr = DbUtils.SafeGetString(reader, "EXPECTED_TIME");
                bool isexpectedTimeStrValid = (expectedDate != null) && (!string.IsNullOrWhiteSpace(expectedTimeStr)) && (expectedTimeStr.Length >= 5);
                if (isexpectedTimeStrValid)
                {
                    try
                    {
                        DateTime expectedDt = DateTime.ParseExact($"{expectedDate?.ToString("yyyy-MM-dd")} {expectedTimeStr[..5]}", "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture);
                        outage.ExpectedDateTime = expectedDt;
                    }
                    catch (FormatException)
                    {
                        continue;
                    }
                }

                outage.OutageReason = DbUtils.SafeGetString(reader, "REASON");
                outage.OutageTag = DbUtils.SafeGetString(reader, "SHUTDOWN_TAG");
                outage.ElementOwners = DbUtils.SafeGetString(reader, "OWNERS");
                outage.InstalledCapacity = DbUtils.SafeGetDouble(reader, "INSTALLED_CAPACITY");
                unrevOutages.Add(outage);
            }
            reader.Dispose();
            return unrevOutages;
        }
    }
}
