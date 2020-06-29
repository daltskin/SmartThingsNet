# SmartThingsNet.Model.Message
A Message contains a list of message templates representing the same message in different locales. The system will serve the template with the locale that best matches the Recipient's language preferences. If the user's language preferences do not correlate to any message templates, the template defined by the `fallbackLocale` will be used. 
## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**FallbackLocale** | **string** | The tag of the locale as defined in [RFC bcp47](http://www.rfc-editor.org/rfc/bcp/bcp47.txt). | 
**Templates** | [**List&lt;MessageTemplate&gt;**](MessageTemplate.md) | A list of templates representing the same message in different languages. | 

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

