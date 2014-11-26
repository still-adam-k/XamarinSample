using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Database;
using Android.OS;
using Android.Provider;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Uri = Android.Net.Uri;
using Xamarin.Contacts;

namespace XamarinSample
{
    [Activity(Label = "ContactListAcitvity")]
    public class ContactListAcitvity : ListActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            var book = new AddressBook(this);
            var contactList = new List<string>();
            book.RequestPermission().ContinueWith(t =>
            {
                if (!t.Result)
                {
                    Console.WriteLine("Permission denied by user or manifest");
                    return;
                }

                foreach (Contact contact in book)
                {
                    var phone = contact.Phones.Any() ? contact.Phones.First().Number : "";
                    contactList.Add(String.Format("{0} {1} {2}", contact.FirstName, contact.LastName, phone));
                }
                ListAdapter = new ArrayAdapter<string>(this,
                Resource.Layout.ContactItem, contactList);
            }, TaskScheduler.FromCurrentSynchronizationContext());
        }
    }
}