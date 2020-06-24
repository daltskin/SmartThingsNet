# Org.OpenAPITools.Model.DeviceCommand
## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**Component** | **string** | The name of the component on this device, default is &#39;main&#39;. The component must be valid for the device. | [optional] [default to "main"]
**Capability** | **string** | Capability that this command relates to. This must be a capability of the component. | 
**Command** | **string** | Name of the command, this must be valid for the capability. | 
**Arguments** | **List&lt;Object&gt;** | Arguments of the command. All the required arguments defined in the capability&#39;s command argument definition must be provided. The type of the arguments are dependent on the type of the capability&#39;s command argument. Please refer to the capabilities definition at https://smartthings.developer.samsung.com/develop/api-ref/capabilities.html  | [optional] 

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

