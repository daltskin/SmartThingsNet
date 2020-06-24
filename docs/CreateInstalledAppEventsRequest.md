# Org.OpenAPITools.Model.CreateInstalledAppEventsRequest
## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**SmartAppEvents** | [**List&lt;SmartAppEventRequest&gt;**](SmartAppEventRequest.md) | An array of smartapp events used to trigger client behavior in loaded web plugin detail pages.  Events will be delivered to JavaScript event handler of all active client processes related to parameterized installed app.  | [optional] 
**SmartAppDashboardCardEvents** | [**List&lt;SmartAppDashboardCardEventRequest&gt;**](SmartAppDashboardCardEventRequest.md) | An array of smartapp dashboard card events used to trigger client behavior for dashboard cards. Dashboard card events are directives to a SmartThings client to take actions in relation to lifecycle changes to a specific dashboard card.  These events are not delivered to the web plugin event handler.  | [optional] 

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

