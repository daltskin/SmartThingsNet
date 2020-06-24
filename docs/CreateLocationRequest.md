# Org.OpenAPITools.Model.CreateLocationRequest
## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**Name** | **string** | A nickname given for the location (eg. Home) | 
**CountryCode** | **string** | An ISO Alpha-3 country code.  (i.e. GBR, USA) | 
**Latitude** | **decimal** | A geographical latitude. | [optional] 
**Longitude** | **decimal** | A geographical longitude. | [optional] 
**RegionRadius** | **int** | The radius in meters around latitude and longitude which defines this location. | [optional] 
**TemperatureScale** | **string** | The desired temperature scale used within location. Value can be F or C. | [optional] 
**Locale** | **string** | An IETF BCP 47 language tag representing the chosen locale for this location. | [optional] 
**AdditionalProperties** | **Dictionary&lt;string, string&gt;** | Additional information about the location that allows SmartThings to further define your location. | [optional] 

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

