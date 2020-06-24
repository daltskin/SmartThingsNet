# Org.OpenAPITools.Model.InstalledAppLifecycleEvent
An Installed App Lifecycle Event.
## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**EventId** | **string** | The id of the event. | [optional] 
**LocationId** | **string** | The ID of the location in which the event was triggered. | [optional] 
**InstalledAppId** | **string** | The ID of the installed application. | [optional] 
**AppId** | **string** | The ID of the application. | [optional] 
**Lifecycle** | **InstalledAppLifecycle** |  | [optional] 
**Create** | **Object** | Create installed app lifecycle.  | [optional] 
**Install** | **Object** | Install installed app lifecycle.  | [optional] 
**Update** | **Object** | Update installed app lifecycle.  | [optional] 
**Delete** | **Object** | Delete installed app lifecycle.  | [optional] 
**Other** | **Object** | Other installed app lifecycle.  | [optional] 
**Error** | [**InstalledAppLifecycleError**](InstalledAppLifecycleError.md) |  | [optional] 

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

