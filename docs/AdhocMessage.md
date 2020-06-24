# Org.OpenAPITools.Model.AdhocMessage
An adhoc message contains a list of message templates representing the same message in different locales. The system will serve the template with the locale that best matches the Recipient's language preferences. If the user's language preferences do not correlate to any message templates, the template defined by the `fallbackLocale` will be used. Variables from the template matching the locale of the Message will be assigned first, then `defaultVariables`. 
## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**FallbackLocale** | **string** | The tag of the locale as defined in [RFC bcp47](http://www.rfc-editor.org/rfc/bcp/bcp47.txt). | 
**DefaultVariables** | **Dictionary&lt;string, string&gt;** | A map&lt;string,string&gt; with the key representing the variable name, and the value representing the verbiage to be replaced in template string. &#x60;defaultVariables&#x60; will only be used if there are no matching locale-level (template) variables for that key.  | [optional] 
**Templates** | [**List&lt;AdhocMessageTemplate&gt;**](AdhocMessageTemplate.md) | A list of templates representing the same message in different languages. | 

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

