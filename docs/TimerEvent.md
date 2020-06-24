# Org.OpenAPITools.Model.TimerEvent
An event that is caused by a schedule for this app being fired.
## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**EventId** | **string** | The ID of the event. | [optional] 
**Name** | **string** | The name of the schedule that caused this event. | [optional] 
**Type** | **TimerType** |  | [optional] 
**Time** | **DateTime** | The IS0-8601 date time strings in UTC that this event was scheduled for. | [optional] 
**Expression** | **string** | The CRON expression if the schedule was of type CRON. | [optional] 

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

