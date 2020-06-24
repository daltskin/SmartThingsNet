# Org.OpenAPITools.Model.SmartAppEventRequest
## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**Name** | **string** | An arbitrary name for the custom SmartApp event.  Typically useful as a hook for in-app routing. | [optional] 
**Attributes** | **Dictionary&lt;string, string&gt;** | An arbitrary set of key / value pairs useful for passing any custom metadata.  * Supports a maximum of 10 entries. * Maximum key length: 36 Unicode characters in UTF-8 * Maximum value length: 256 Unicode characters in UTF-8 * Allowed characters for *keys* are letters, plus the following special characters: &#x60;:&#x60;, &#x60;_&#x60; * Allowed characters for *values* are letters, whitespace, and numbers, plus the following special characters: &#x60;+&#x60;, &#x60;-&#x60;, &#x60;&#x3D;&#x60;, &#x60;.&#x60;, &#x60;_&#x60;, &#x60;:&#x60;, &#x60;/&#x60; * If you need characters outside this allowed set, you can apply standard base-64 encoding.  | [optional] 

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

