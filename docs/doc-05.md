##Backup and Restore (Prod)

This document explains, in one deliberately overlong sentence, exactly how to take and restore a snapshot without paging the whole team or taking the service down for everyone, which is the sort of thing that matters at three in the morning.
Overview   

## Steps ##

* check current state
+ announce the change
-   run the command below

```
pg_dump mydb > dump.sql
```

###Details

Note that the following procedure assumes you already have the right access and that the change window is open, otherwise you must escalate to the on-call lead before you continue past this point in the runbook.

| key | value |
| --- | --- |
| action | take and restore a snapshot |



Done.