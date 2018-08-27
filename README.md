# UiPath.ServiceNow

ServiceNow Activities for UiPath. All activities must be enlosed within "ServiceNow Connector Scope". User must have the REST API access
to the ServiceNow Instance

## Activities

1. Get Incident Data
2. Get Incidents
3. Update Incident

## Get Incident Data

Gets the details of the requested Incident

### Input

Incident Number

### Output

IncidentObject - Incident details of type JObject

## Get Incidents

Get all the Incidents matching the sysparm query. If sysparm_query is empty, all incidents will be fetched.

### Input

SysParm_query - Filtering criteria. For example, List of incidents under a particular assignment group with Assigned status.

### Output
IncidentList - List of incidents of type JArray.

## Update Incident  

Updates the particular Incident with the details provided in body in JSON string format.

### Input

IncidentSysId - Sys_id of the Incident number
Body (JSON String) - Data to be updated for the incident. Details to be provided in JSON string format.

### Output

IncidentObject - Updated Incident data of type JObject
