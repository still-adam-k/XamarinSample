using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Provider;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace XamarinSample
{
    [Activity(Label = "AddContactActivity")]
    public class AddContactActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.AddContact);

            FindViewById<Button>(Resource.Id.btnSaveContact).Click += delegate { SaveContact(); };
            FindViewById<Button>(Resource.Id.btnCheckContactExists).Click += delegate { CheckIfContactExists(); };
        }

        public void SaveContact()
        {
            // overview on contacts provider - http://developer.android.com/guide/topics/providers/contacts-provider.html

            var name = FindViewById<EditText>(Resource.Id.etName).Text;
            var phone = FindViewById<EditText>(Resource.Id.etPhone).Text;

            List<ContentProviderOperation> ops = new List<ContentProviderOperation>();

            ContentProviderOperation.Builder builder =
                ContentProviderOperation.NewInsert(ContactsContract.RawContacts.ContentUri);
            //For update use -  ContentProviderOperation.NewUpdate(ContactsContract.RawContacts.ContentUri);

            builder.WithValue(ContactsContract.RawContacts.InterfaceConsts.AccountType, "twitter.com");
            builder.WithValue(ContactsContract.RawContacts.InterfaceConsts.AccountName, "myName@myEmail.com");
            ops.Add(builder.Build());

            //Name
            builder = ContentProviderOperation.NewInsert(ContactsContract.Data.ContentUri);
            builder.WithValueBackReference(ContactsContract.Data.InterfaceConsts.RawContactId, 0);
            builder.WithValue(ContactsContract.Data.InterfaceConsts.Mimetype,
                              ContactsContract.CommonDataKinds.StructuredName.ContentItemType);
            builder.WithValue(ContactsContract.CommonDataKinds.StructuredName.FamilyName, name);
            builder.WithValue(ContactsContract.CommonDataKinds.StructuredName.DisplayName, name);
            ops.Add(builder.Build());

            //Number
            builder = ContentProviderOperation.NewInsert(ContactsContract.Data.ContentUri);
            builder.WithValueBackReference(ContactsContract.Data.InterfaceConsts.RawContactId, 0);
            builder.WithValue(ContactsContract.Data.InterfaceConsts.Mimetype,
                              ContactsContract.CommonDataKinds.Phone.ContentItemType);
            builder.WithValue(ContactsContract.CommonDataKinds.Phone.Number, phone);
            builder.WithValue(ContactsContract.CommonDataKinds.Phone.InterfaceConsts.Type,
                              ContactsContract.CommonDataKinds.Phone.InterfaceConsts.TypeCustom);
            builder.WithValue(ContactsContract.CommonDataKinds.Phone.InterfaceConsts.Label, "Work");
            ops.Add(builder.Build());

            //Add the new contact
            try
            {
                ContentResolver.ApplyBatch(ContactsContract.Authority, ops);
                Toast.MakeText(this, "Contact saved", ToastLength.Short).Show();
            }
            catch
            {
                Toast.MakeText(this, "Contact somehow failed to save", ToastLength.Short).Show();
            }
        }

        public void CheckIfContactExists()
        {
            var name = FindViewById<EditText>(Resource.Id.etName).Text;

            Android.Net.Uri uri = ContactsContract.Contacts.ContentUri;

            string[] projection = { ContactsContract.Contacts.InterfaceConsts.DisplayName };
            String selection = string.Format("{0} = '{1}'", ContactsContract.ContactsColumns.DisplayName, name);

            var cursor = ManagedQuery(uri, projection, selection, null, null);

            if (cursor.Count <= 0)
            {
                Toast.MakeText(this, "Contact does not exist", ToastLength.Long).Show();
            }
            else
            {
                Toast.MakeText(this, "Contact exists", ToastLength.Long).Show();
            }
        }
    }
}