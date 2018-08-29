# UiPath.ServiceNow

ServiceNow Activities for UiPath. All activities must be enlosed within "ServiceNow Connector Scope". User must have the REST API access
to the ServiceNow Instance

## Activities

1. Create Incident
2. Get Incident Data
3. Get Incidents
4. Update Incident

## Create Incident

Creates a new incident with the details provided in json format

### Input

Body (JSON string) - Data for the new incident

Example - "{""caller_id"":""Andrew Jackson"",""short_description"":""Network drives X and Y not accessible""}"

## Output

IncidentObject - New Incident created and the details are retrieved in JObject format

## Get Incident Data

Gets the details of the requested Incident

### Input

Incident Number

### Output

IncidentJObject - Incident details of type JObject

## Get Incidents

Get all the Incidents matching the sysparm query. If sysparm_query is empty, all incidents will be fetched.

### Input

SysParm_query - Filtering criteria. For example, List of incidents under a particular assignment group with Assigned status.

Example - "assignment_group=287ebd7da9fe198100f92cc8d1d2154e"

### Output
IncidentList - List of incidents of type JArray.

## Update Incident  

Updates the particular Incident with the details provided in body in JSON string format.

### Input

IncidentSysId - Sys_id of the Incident number
Body (JSON String) - Data to be updated for the incident. Details to be provided in JSON string format.

Example - "{""state"":""6"",""close_code"": ""Solved (Work Around)"",""close_notes"":""note2""}"

### Output

IncidentObject - Updated Incident data of type JObject
