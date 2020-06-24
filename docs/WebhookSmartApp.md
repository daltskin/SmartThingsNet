# Org.OpenAPITools.Model.WebhookSmartApp
Details related to a Webhook Smart App implementation.  This model will only be available for apps of type WEBHOOK_SMART_APP. 
## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**TargetUrl** | **string** | A URL that should be invoked during execution. | [optional] 
**TargetStatus** | **AppTargetStatus** |  | [optional] 
**PublicKey** | **string** | The public half of an RSA key pair.  Useful for verifying a Webhook execution request signature to ensure it came from SmartThings.  | [optional] 
**SignatureType** | **SignatureType** |  | [optional] 

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

