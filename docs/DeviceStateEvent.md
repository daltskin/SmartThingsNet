# SmartThingsNet.Model.DeviceStateEvent
## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**Component** | **string** | The name of the component on this device, default is &#39;main&#39;. | [optional] 
**Capability** | **string** | Capability that this event relates to. | [optional] 
**Attribute** | **string** | Name of the capability attribute that this event relates to. | [optional] 
**Value** | **Object** | Value associated with the event. The valid value depends on the capability. | 
**Unit** | **string** | Unit of the value field. | [optional] 
**Data** | **Dictionary&lt;string, Object&gt;** | Key value pairs about the state of the device. Valid values depend on the capability. | [optional] 

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

