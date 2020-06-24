# SmartThingsNet.Model.Argument
## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**Name** | **string** | A name that is unique within the command. Used for i18n and named argument command execution. | 
**Optional** | **bool** | Whether or not the argument must be supplied. If the argument is at the end of the arguments array then it can be completely ignored. If the argument is followed by another argument &#x60;null&#x60; must be supplied.  | [optional] [default to false]
**Schema** | **Object** | [JSON schema](http://json-schema.org/specification-links.html#draft-4) for the argument.  | 

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

