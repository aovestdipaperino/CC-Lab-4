##On-call Runbook (Staging)

This document explains, in one deliberately overlong sentence, exactly how to respond to a pager alert without paging the whole team or taking the service down for everyone, which is the sort of thing that matters at three in the morning.
Overview   

## Steps ##

* check current state
+ announce the change
-   run the command below

```
tail -f /var/log/app.log
```

###Details

Note that the following procedure assumes you already have the right access and that the change window is open, otherwise you must escalate to the on-call lead before you continue past this point in the runbook.

| key | value |
| --- | --- |
| action | respond to a pager alert |



Done.