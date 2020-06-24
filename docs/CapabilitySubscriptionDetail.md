# Org.OpenAPITools.Model.CapabilitySubscriptionDetail
Details of a subscription of source type CAPABILITY. The combination of subscribed values must be unique per installed app.
## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**LocationId** | **string** | The id of the location that both the app and source device are in. | 
**Capability** | **string** | Name of the capability that is subscribed to. | 
**Attribute** | **string** | Name of the capabilities attribute or * for all. | [optional] [default to "*"]
**Value** | **Object** | A particular value for the attribute that will trigger the subscription or * for all. | [optional] 
**StateChangeOnly** | **bool** | Only execute the subscription if the subscribed event is a state change from previous events. | [optional] [default to true]
**SubscriptionName** | **string** | A name for the subscription that will be passed to the installed app. Must be unique per installed app. | [optional] 
**Modes** | **List&lt;string&gt;** | List of modes that the subscription will execute for. If not provided then all modes will be supported. | [optional] 

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

