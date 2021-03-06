﻿//* Copyright 2010-2011 Research In Motion Limited.
//*
//* Licensed under the Apache License, Version 2.0 (the "License");
//* you may not use this file except in compliance with the License.
//* You may obtain a copy of the License at
//*
//* http://www.apache.org/licenses/LICENSE-2.0
//*
//* Unless required by applicable law or agreed to in writing, software
//* distributed under the License is distributed on an "AS IS" BASIS,
//* WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//* See the License for the specific language governing permissions and
//* limitations under the License.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Diagnostics;

namespace VSNDK.Tasks.BarDescriptor
{
    public class Parser
    {
        /// <summary>
        /// Helper function to to deserialize the bar-descriptor.xml file for read.
        /// </summary>
        /// <param name="filename">Path to the bar-descriptor.xml file</param>
        /// <returns>BarDescriptor.qnx object containing the deserialized data from the bar-descriptor.</returns>
        public static  BarDescriptor.qnx Load(string filename)
        {
            if (!string.IsNullOrEmpty(filename))
            {
                try
                {
                    using (FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read))
                    {
                        XmlTextReader reader = new XmlTextReader(fs);
                        XmlSerializer serializer = new XmlSerializer(typeof(BarDescriptor.qnx));
                        return serializer.Deserialize(reader) as BarDescriptor.qnx;
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
            }
            return null;
        }
    }
}
