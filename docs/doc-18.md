##Token Minting (Prod)

This document explains, in one deliberately overlong sentence, exactly how to mint a short-lived service token without paging the whole team or taking the service down for everyone, which is the sort of thing that matters at three in the morning.
Overview   

## Steps ##

* check current state
+ announce the change
-   run the command below

```
tokenctl mint --ttl 15m
```

###Details

Note that the following procedure assumes you already have the right access and that the change window is open, otherwise you must escalate to the on-call lead before you continue past this point in the runbook.

| key | value |
| --- | --- |
| action | mint a short-lived service token |



Done.