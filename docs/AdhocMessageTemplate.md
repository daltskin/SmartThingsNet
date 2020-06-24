# SmartThingsNet.Model.AdhocMessageTemplate
A message template definition, representing a message in a specific locale and it's variables.
## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**LocaleTag** | **string** | The tag of the locale as defined in [RFC bcp47](http://www.rfc-editor.org/rfc/bcp/bcp47.txt). | 
**Variables** | **Dictionary&lt;string, string&gt;** | A map&lt;string,string&gt; with the key representing the variable name, and the value representing the verbiage to be replaced in template string.  | [optional] 
**Template** | **string** | A message template string.  Specify variables using the double curly braces convention. i.e. \&quot;Hello, {{ firstName }}!\&quot;  | 

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

