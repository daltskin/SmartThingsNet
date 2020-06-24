# SmartThingsNet.Model.DeviceLifecycleEvent
A device lifecycle event.
## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**Lifecycle** | **DeviceLifecycle** |  | [optional] 
**EventId** | **string** | The id of the event. | [optional] 
**LocationId** | **string** | The id of the location in which the event was triggered. | [optional] 
**DeviceId** | **string** | The id of the device. | [optional] 
**DeviceName** | **string** | The name of the device | [optional] 
**Principal** | **string** | The principal that made the change | [optional] 
**Create** | **Object** | Create device lifecycle.  | [optional] 
**Delete** | **Object** | Delete device lifecycle.  | [optional] 
**Update** | **Object** | Update device lifecycle.  | [optional] 
**MoveFrom** | [**DeviceLifecycleMove**](DeviceLifecycleMove.md) |  | [optional] 
**MoveTo** | [**DeviceLifecycleMove**](DeviceLifecycleMove.md) |  | [optional] 

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

