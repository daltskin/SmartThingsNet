# Org.OpenAPITools.Model.CapabilitySummary
## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**Id** | **string** | A URL safe unique identifier for the capability. | [optional] 
**Version** | **int** | The version number of the capability. | [optional] 
**Status** | **string** | The status of the capability. * __proposed__ - The capability is under a review and refinement process. The capability definition may go through changes, some of which may be breaking. * __live__ - The capability has been through review and the definition has been solidified. Live capabilities can no longer be altered. * __deprecated__ - The capability is marked for removal and should only be used during a period of migration to allow for existing integrations and automations to continue to work. * __dead__ - The usage of a deprecated capability has dropped to a sufficiently low level to warrant removal. The capability definition still exists but can no longer be used by automations or implemented by devices.  | [optional] 

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

