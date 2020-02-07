// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using Microsoft.Toolkit.Uwp.UI.Controls;
using Microsoft.Toolkit.Uwp.UI.Extensions;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Microsoft.Toolkit.Uwp.SampleApp.SamplePages
{
    public sealed partial class TokenizingTextBoxPage : Page, IXamlRenderListener, INotifyPropertyChanged
    {
        private TokenizingTextBox _ttb;
        private TokenizingTextBox _ttbEmail;

        /// <summary>
        /// Change notification event
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Gets an observable List of names for the suppressed suggestion list control
        /// </summary>
        public ObservableCollection<SampleEmailDataType> EmailSelectionList
        {
            get
            {
                if (_ttbEmail != null)
                {
                    List<SampleEmailDataType> removeList = new List<SampleEmailDataType>();

                    // add the tokens in the token list already
                    var listitems = _ttbEmail.Items.Cast<SampleEmailDataType>();
                    removeList.AddRange(listitems);
                    if (!string.IsNullOrWhiteSpace(_ttbEmail.Text))
                    {
                        removeList.AddRange(_emailSamples.Where((item) => !item.DisplayName.Contains(_ttbEmail.Text, System.StringComparison.CurrentCultureIgnoreCase)));
                    }

                    return new ObservableCollection<SampleEmailDataType>(_emailSamples.Except(removeList).OrderBy(item => item.DisplayName));
                }
                else
                {
                    return new ObservableCollection<SampleEmailDataType>(_emailSamples.OrderBy(item => item.DisplayName));
                }
            }
        }

        private readonly List<SampleEmailDataType> _emailSamples = new List<SampleEmailDataType>()
        {
            new SampleEmailDataType() { FirstName = "Marcus", FamilyName = "Perryman", Icon = Symbol.Account },
            new SampleEmailDataType() { FirstName = "Ian", FamilyName = "Smith", Icon = Symbol.AddFriend },
            new SampleEmailDataType() { FirstName = "Peter", FamilyName = "Strange", Icon = Symbol.Attach },
            new SampleEmailDataType() { FirstName = "Alex", FamilyName = "Wilber", Icon = Symbol.AttachCamera },
            new SampleEmailDataType() { FirstName = "Allan", FamilyName = "Deyoung", Icon = Symbol.Audio },
            new SampleEmailDataType() { FirstName = "Adele", FamilyName = "Vance", Icon = Symbol.BlockContact },
            new SampleEmailDataType() { FirstName = "Grady", FamilyName = "Archie", Icon = Symbol.Calculator },
            new SampleEmailDataType() { FirstName = "Megan", FamilyName = "Bowen", Icon = Symbol.Calendar },
            new SampleEmailDataType() { FirstName = "Ben", FamilyName = "Walters", Icon = Symbol.Camera },
            new SampleEmailDataType() { FirstName = "Debra", FamilyName = "Berger", Icon = Symbol.Contact },
            new SampleEmailDataType() { FirstName = "Emily", FamilyName = "Braun", Icon = Symbol.Favorite },
            new SampleEmailDataType() { FirstName = "Christine", FamilyName = "Cline", Icon = Symbol.Link },
            new SampleEmailDataType() { FirstName = "Enrico", FamilyName = "Catteneo", Icon = Symbol.Mail },
            new SampleEmailDataType() { FirstName = "Davit", FamilyName = "Badalyan", Icon = Symbol.Map },
            new SampleEmailDataType() { FirstName = "Diego", FamilyName = "Siciliani", Icon = Symbol.Phone },
            new SampleEmailDataType() { FirstName = "Raul", FamilyName = "Razo", Icon = Symbol.Pin },
            new SampleEmailDataType() { FirstName = "Miriam", FamilyName = "Graham", Icon = Symbol.Rotate },
            new SampleEmailDataType() { FirstName = "Lynne", FamilyName = "Robbins", Icon = Symbol.RotateCamera },
            new SampleEmailDataType() { FirstName = "Lydia", FamilyName = "Holloway", Icon = Symbol.Send },
            new SampleEmailDataType() { FirstName = "Nestor", FamilyName = "Wilke", Icon = Symbol.Tag },
            new SampleEmailDataType() { FirstName = "Patti", FamilyName = "Fernandez", Icon = Symbol.UnFavorite },
            new SampleEmailDataType() { FirstName = "Pradeep", FamilyName = "Gupta", Icon = Symbol.UnPin },
            new SampleEmailDataType() { FirstName = "Joni", FamilyName = "Sherman", Icon = Symbol.Zoom },
            new SampleEmailDataType() { FirstName = "Isaiah", FamilyName = "Langer", Icon = Symbol.ZoomIn },
            new SampleEmailDataType() { FirstName = "Irvin", FamilyName = "Sayers", Icon = Symbol.ZoomOut },
        };

        private List<SampleDataType> _samples = new List<SampleDataType>()
        {
            new SampleDataType() { Text = "Account", Icon = Symbol.Account },
            new SampleDataType() { Text = "Add Friend", Icon = Symbol.AddFriend },
            new SampleDataType() { Text = "Attach", Icon = Symbol.Attach },
            new SampleDataType() { Text = "Attach Camera", Icon = Symbol.AttachCamera },
            new SampleDataType() { Text = "Audio", Icon = Symbol.Audio },
            new SampleDataType() { Text = "Block Contact", Icon = Symbol.BlockContact },
            new SampleDataType() { Text = "Calculator", Icon = Symbol.Calculator },
            new SampleDataType() { Text = "Calendar", Icon = Symbol.Calendar },
            new SampleDataType() { Text = "Camera", Icon = Symbol.Camera },
            new SampleDataType() { Text = "Contact", Icon = Symbol.Contact },
            new SampleDataType() { Text = "Favorite", Icon = Symbol.Favorite },
            new SampleDataType() { Text = "Link", Icon = Symbol.Link },
            new SampleDataType() { Text = "Mail", Icon = Symbol.Mail },
            new SampleDataType() { Text = "Map", Icon = Symbol.Map },
            new SampleDataType() { Text = "Phone", Icon = Symbol.Phone },
            new SampleDataType() { Text = "Pin", Icon = Symbol.Pin },
            new SampleDataType() { Text = "Rotate", Icon = Symbol.Rotate },
            new SampleDataType() { Text = "Rotate Camera", Icon = Symbol.RotateCamera },
            new SampleDataType() { Text = "Send", Icon = Symbol.Send },
            new SampleDataType() { Text = "Tags", Icon = Symbol.Tag },
            new SampleDataType() { Text = "UnFavorite", Icon = Symbol.UnFavorite },
            new SampleDataType() { Text = "UnPin", Icon = Symbol.UnPin },
            new SampleDataType() { Text = "Zoom", Icon = Symbol.Zoom },
            new SampleDataType() { Text = "ZoomIn", Icon = Symbol.ZoomIn },
            new SampleDataType() { Text = "ZoomOut", Icon = Symbol.ZoomOut },
        };

        public TokenizingTextBoxPage()
        {
            InitializeComponent();
            Loaded += (sernder, e) => { this.OnXamlRendered(this); };
        }

        public void OnXamlRendered(FrameworkElement control)
        {
            // For the basic control
            if (_ttb != null)
            {
                _ttb.TokenItemAdded -= TokenItemAdded;
                _ttb.TokenItemRemoved -= TokenItemRemoved;
                _ttb.TextChanged -= TextChanged;
                _ttb.TokenItemCreating -= TokenItemCreating;
            }

            if (control.FindChildByName("TokenBox") is TokenizingTextBox ttb)
            {
                _ttb = ttb;

                _ttb.TokenItemAdded += TokenItemAdded;
                _ttb.TokenItemRemoved += TokenItemRemoved;
                _ttb.TextChanged += TextChanged;
                _ttb.TokenItemCreating += TokenItemCreating;
            }

            // For the Email Selection control
            if (_ttbEmail != null)
            {
                _ttbEmail.TokenItemAdded -= EmailTokenItemAdded;
                _ttbEmail.TokenItemRemoved -= EmailTokenItemRemoved;
                _ttbEmail.TextChanged -= EmailTextChanged;
            }

            if (control.FindChildByName("TokenBoxWhatsApp") is TokenizingTextBox ttbWA)
            {
                _ttbEmail = ttbWA;

                _ttbEmail.TokenItemAdded += EmailTokenItemAdded;
                _ttbEmail.TokenItemRemoved += EmailTokenItemRemoved;
                _ttbEmail.TextChanged += EmailTextChanged;
            }

            EmailList.DataContext = this;
        }

        private void TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            if (args.CheckCurrent() && args.Reason == AutoSuggestionBoxTextChangeReason.UserInput)
            {
                if (string.IsNullOrWhiteSpace(sender.Text))
                {

                    _ttb.SuggestedItemsSource = Array.Empty<object>();
                }
                else
                {
                    _ttb.SuggestedItemsSource = _samples.Where((item) => item.Text.Contains(sender.Text, System.StringComparison.CurrentCultureIgnoreCase)).OrderByDescending(item => item.Text);
                }
            }
        }

        private void TokenItemCreating(object sender, TokenItemCreatingEventArgs e)
        {
            // Take the user's text and convert it to our data type.
            e.Item = _samples.FirstOrDefault((item) => item.Text.Contains(e.TokenText, System.StringComparison.CurrentCultureIgnoreCase));
        }

        private void TokenItemAdded(TokenizingTextBox sender, TokenizingTextBoxItem args)
        {
            // TODO: Add InApp Notification?
            if (args.Content is SampleDataType sample)
            {
                Debug.WriteLine("Added Token: " + sample.Text);
            }
            else
            {
                Debug.WriteLine("Added Token: " + args.Content);
            }
        }

        private void TokenItemRemoved(TokenizingTextBox sender, TokenItemRemovedEventArgs args)
        {
            if (args.Item is SampleDataType sample)
            {
                Debug.WriteLine("Removed Token: " + sample.Text);
            }
            else
            {
                Debug.WriteLine("Removed Token: " + args.Item);
            }
        }

        private void EmailTextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            if (args.CheckCurrent() && args.Reason == AutoSuggestionBoxTextChangeReason.UserInput)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("EmailSelectionList"));
            }
        }

        private void EmailTokenItemAdded(TokenizingTextBox sender, TokenizingTextBoxItem args)
        {
            if (args.Content is SampleEmailDataType sample)
            {
                Debug.WriteLine("Added Email: " + sample.DisplayName);
            }
            else
            {
                Debug.WriteLine("Added Token: " + args.Content);
            }
        }

        private void EmailTokenItemRemoved(TokenizingTextBox sender, TokenItemRemovedEventArgs args)
        {
            if (args.Item is SampleEmailDataType sample)
            {
                Debug.WriteLine("Removed Email: " + sample.DisplayName);
            }
            else
            {
                Debug.WriteLine("Removed Token: " + args.Item);
            }

            // Refresh the list of suggestions
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("EmailSelectionList"));
        }

        private async void EmailList_ItemClick(object sender, ItemClickEventArgs e)
        {
            await _ttbEmail.AddItem(e.ClickedItem);
            _ttbEmail.Text = string.Empty;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("EmailSelectionList"));
        }

        private void ClearButtonClick(object sender, RoutedEventArgs e)
        {
            _ttbEmail.ClearItems();
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("EmailSelectionList"));
        }
    }
}
