# SmartThingsNet.Model.App
## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**AppName** | **string** | A user defined unique identifier for an app.  It is alpha-numeric, may contain dashes, underscores, periods, and be less then 250 characters long.  It must be unique within your account.  | [optional] 
**AppId** | **Guid** | A globally unique identifier for an app. | [optional] 
**AppType** | **AppType** |  | [optional] 
**PrincipalType** | **PrincipalType** |  | [optional] 
**Classifications** | [**List&lt;AppClassification&gt;**](AppClassification.md) | An App maybe associated to many classifications.  A classification drives how the integration is presented to the user in the SmartThings mobile clients.  These classifications include: * AUTOMATION - Denotes an integration that should display under the \&quot;Automation\&quot; tab in mobile clients. * SERVICE - Denotes an integration that is classified as a \&quot;Service\&quot;. * DEVICE - Denotes an integration that should display under the \&quot;Device\&quot; tab in mobile clients. * CONNECTED_SERVICE - Denotes an integration that should display under the \&quot;Connected Services\&quot; menu in mobile clients. * HIDDEN - Denotes an integration that should not display in mobile clients  | [optional] 
**DisplayName** | **string** | A default display name for an app.  | [optional] 
**Description** | **string** | A default description for an app.  | [optional] 
**SingleInstance** | **bool** | Inform the installation systems that a particular app can only be installed once within a user&#39;s account.  | [optional] [default to false]
**IconImage** | [**IconImage**](IconImage.md) |  | [optional] 
**InstallMetadata** | **Dictionary&lt;string, string&gt;** | System generated metadata that impacts eligibility requirements around installing an App. | [optional] 
**Owner** | [**Owner**](Owner.md) |  | [optional] 
**CreatedDate** | **DateTime** | A UTC ISO-8601 Date-Time String | [optional] 
**LastUpdatedDate** | **DateTime** | A UTC ISO-8601 Date-Time String | [optional] 
**LambdaSmartApp** | [**LambdaSmartApp**](LambdaSmartApp.md) |  | [optional] 
**WebhookSmartApp** | [**WebhookSmartApp**](WebhookSmartApp.md) |  | [optional] 
**Ui** | [**AppUISettings**](AppUISettings.md) |  | [optional] 

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

