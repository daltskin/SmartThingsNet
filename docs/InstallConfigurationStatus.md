# Org.OpenAPITools.Model.InstallConfigurationStatus
Denotes the current state of a configuration instance.  'STAGED' configuration is used during active modification to config.  A configuration is marked 'DONE' once it is deemed finished.  At this point it is immutable, meaning it can't be changed.  A status of 'AUTHORIZED' means the apps permissions have been authorized by the consumer.  Installed Apps in 'AUTHORIZED' state are fully installed and used by the SmartThings platform.  A status of 'REVOKED' means the apps permissions have been revoked. 
## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

