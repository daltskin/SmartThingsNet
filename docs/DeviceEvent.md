# Org.OpenAPITools.Model.DeviceEvent
An event on a device that matched a subscription for this app.
## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**EventId** | **string** | The ID of the event. | [optional] 
**LocationId** | **string** | The ID of the location in which the event was triggered. | [optional] 
**DeviceId** | **string** | The ID of the device associated with the DEVICE_EVENT. | [optional] 
**ComponentId** | **string** | The name of the component on the device that the event is associated with. | [optional] 
**Capability** | **string** | The name of the capability associated with the DEVICE_EVENT. | [optional] 
**Attribute** | **string** | The name of the DEVICE_EVENT. This typically corresponds to an attribute name of the device-handler’s capabilities. | [optional] 
**Value** | **Object** | The value of the event. The type of the value is dependent on the capability&#39;s attribute type.  | [optional] 
**ValueType** | **string** | The root level data type of the value field. The data types are representitive of standard JSON data types.  | [optional] 
**StateChange** | **bool** | Whether or not the state of the device has changed as a result of the DEVICE_EVENT. | [optional] 
**Data** | **Object** | json map as defined by capability data schema | [optional] 
**SubscriptionName** | **string** | The name of subscription that caused delivery. | [optional] 

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

