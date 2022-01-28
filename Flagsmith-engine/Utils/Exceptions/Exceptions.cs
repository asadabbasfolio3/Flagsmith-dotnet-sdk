﻿using System;
using System.Collections.Generic;
using System.Text;

namespace FlagsmithEngine.Exceptions
{
    class FeatureStateNotFound : Exception
    {
        //Overriding the Message property
        public override string Message
        {
            get
            {
                return "Feature State not found.";
            }
        }
    }
    public class DuplicateFeatureState : Exception
    {
    }
    public class InvalidPercentageAllocation : Exception
    {
    }
}
