# Org.OpenAPITools.Model.CronSchedule
## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**Expression** | **string** | The cron expression for the schedule for CRON schedules. The format matches that specified by the [Quartz scheduler](http://www.quartz-scheduler.org/documentation/quartz-2.x/tutorials/crontrigger.html) but should not include the seconds (1st) field. The exact second will be chosen at random but will remain consistent. The years part must be les than 2 years from now.  | 
**Timezone** | **string** | The timezone id for CRON schedules. | 

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

