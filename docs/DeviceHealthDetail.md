# Org.OpenAPITools.Model.DeviceHealthDetail
Details of a subscription of source type DEVICE_HEALTH. Only one of deviceIds or locationId should be supplied.
## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**DeviceIds** | **List&lt;string&gt;** | An array of GUIDs of devices being subscribed to. A max of 20 GUIDs are allowed. | [optional] 
**SubscriptionName** | **string** | A name for the subscription that will be passed to the installed app. | [optional] 
**LocationId** | **string** | The id of the location that both the app and source device are in. | [optional] 

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

