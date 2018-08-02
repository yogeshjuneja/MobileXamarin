﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;
using MapKit;

namespace EMeditekApp.iOS
{
    class CustomMKAnnotationView : MKAnnotationView
    {
        public string Id { get; set; }

        public string Phone { get; set; }

        public CustomMKAnnotationView(IMKAnnotation annotation, string id)
            : base(annotation, id)
        {
        }
    }
}