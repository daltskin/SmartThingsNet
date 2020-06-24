# Org.OpenAPITools.Model.DeviceCommandsEvent
An event that contains commands for devices that were created by this app.
## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**EventId** | **string** | The id of the event. | [optional] 
**DeviceId** | **string** | The guid of the device that the commands are for. | [optional] 
**ProfileId** | **string** | The device profile ID of the device instance. | [optional] 
**ExternalId** | **string** | The external ID that was set during install of a device. | [optional] 
**Commands** | [**List&lt;DeviceCommandsEventCommand&gt;**](DeviceCommandsEventCommand.md) |  | [optional] 

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

