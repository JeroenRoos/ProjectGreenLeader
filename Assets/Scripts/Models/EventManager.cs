﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class EventManager
{
    // Event Actions
    public delegate void MonthChanged();
    public static event MonthChanged ChangeMonth;

    public delegate void EventOccured();
    public static event EventOccured ShowEvent;


    public static void CallChangeMonth()
    {
        ChangeMonth();
    }

    public static void CallShowEvent()
    {
        ShowEvent();
    }

}