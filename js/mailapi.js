'use strict';

// run like this:
//   GMAIL_USER="my.user@gmail.com" GMAIL_PASS="my-pass" node compression-example.js

var ImapClient = require('src/emailjs-imap-client.js');

var client = new ImapClient('imap.gmail.com', 993, {
    useSecureTransport: true,
    auth: {
        user: "thomasp@abeo-electra.com",
        pass: "Peterrajt1"
    },
    enableCompression: true
});

client.onerror = function(err) {
    console.log(err);
};

client.connect().then(() => {
    client.listMailboxes().then((mailboxes) => {
        console.log(mailboxes);
    }).then(() => {
        client.logout();
    });
});