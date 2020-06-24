# Org.OpenAPITools.Model.UpdateAppRequest
## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**DisplayName** | **string** | A default display name for an app.  | 
**Description** | **string** | A default description for an app.  | 
**SingleInstance** | **bool** | Inform the installation systems that a particular app can only be installed once within a user&#39;s account.  | [optional] [default to false]
**IconImage** | [**IconImage**](IconImage.md) |  | [optional] 
**AppType** | **AppType** |  | 
**Classifications** | [**List&lt;AppClassification&gt;**](AppClassification.md) | An App maybe associated to many classifications.  A classification drives how the integration is presented to the user in the SmartThings mobile clients.  These classifications include: * AUTOMATION - Denotes an integration that should display under the \&quot;Automation\&quot; tab in mobile clients. * SERVICE - Denotes an integration that is classified as a \&quot;Service\&quot;. * DEVICE - Denotes an integration that should display under the \&quot;Device\&quot; tab in mobile clients. * CONNECTED_SERVICE - Denotes an integration that should display under the \&quot;Connected Services\&quot; menu in mobile clients. * HIDDEN - Denotes an integration that should not display in mobile clients  | 
**LambdaSmartApp** | [**CreateOrUpdateLambdaSmartAppRequest**](CreateOrUpdateLambdaSmartAppRequest.md) |  | [optional] 
**WebhookSmartApp** | [**CreateOrUpdateWebhookSmartAppRequest**](CreateOrUpdateWebhookSmartAppRequest.md) |  | [optional] 
**Ui** | [**AppUISettings**](AppUISettings.md) |  | [optional] 

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

