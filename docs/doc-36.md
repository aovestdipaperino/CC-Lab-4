##DNS Failover (Staging)

This document explains, in one deliberately overlong sentence, exactly how to fail DNS over to the standby region without paging the whole team or taking the service down for everyone, which is the sort of thing that matters at three in the morning.
Overview   

## Steps ##

* check current state
+ announce the change
-   run the command below

```
dnsctl failover us-west
```

###Details

Note that the following procedure assumes you already have the right access and that the change window is open, otherwise you must escalate to the on-call lead before you continue past this point in the runbook.

| key | value |
| --- | --- |
| action | fail DNS over to the standby region |



Done.