﻿using System;
using Xamarin.Forms;

namespace EMeditekApp.Wellogo
{
    public class EntryLengthValidatorBehavior:Behavior<Entry>
    {
        public int MaxLength { get; set; }

        protected override void OnAttachedTo(Entry bindable)
        {
            base.OnAttachedTo(bindable);
            bindable.TextChanged += OnEntryTextChanged;
        }

        protected override void OnDetachingFrom(Entry bindable)
        {
            base.OnDetachingFrom(bindable);
            bindable.TextChanged -= OnEntryTextChanged;
        }

        void OnEntryTextChanged(object sender, TextChangedEventArgs e)
        {

             try
            {
                var entry = (Entry)sender;
                if (entry.Text != "" && entry.Text!=null)
                {
                    // if Entry text is longer then valid length
                    if (entry.Text.Length > this.MaxLength)
                    {
                        string entryText = entry.Text;

                        entryText = entryText.Remove(entryText.Length - 1); // remove last char

                        entry.Text = entryText;
                    }
                }
            }
            catch (Exception ex)
            {

            }
          
        }
    }
}
