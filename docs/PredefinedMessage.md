# Org.OpenAPITools.Model.PredefinedMessage
Predefined message options. Variables from `localeVariables` matching the locale of the Message will be assigned first, then `defaultVariables`. 
## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**MessageTemplateKey** | **string** |  | 
**DefaultVariables** | **Dictionary&lt;string, string&gt;** | A map&lt;string,string&gt; with the key representing the variable name, and the value representing the verbiage to be replaced in template string. &#x60;defaultVariables&#x60; are only used when there are no matching &#x60;localeVariables&#x60;.  | [optional] 
**LocaleVariables** | [**List&lt;LocaleVariables&gt;**](LocaleVariables.md) | Variables to resolve for specific locales.  | [optional] 

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

