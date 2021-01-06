using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Core.Models
{
    public static class DomainErrorCodes
    {
        public static string InvalidEmail => "invalid_email";
        public static string InvalidPassword => "invalid_password";
        public static string InvalidRole => "invalid_role";
        public static string InvalidName => "invalid_name";
        public static string InvalidUsername => "invalid_username";
        public static string InvalidFirstName => "invalid_first_name";
        public static string InvalidLastName => "invalid_last_name";
        public static string InvalidTitle => "invalid_title";
        public static string InvalidDescription => "invalid_description";
        public static string InvalidQuantity => "invalid_quantity";
        public static string InvalidDate => "invalid_date";
        public static string InvalidAuthor => "invalid_author";
        public static string InvalidHouse => "invalid_house";
        public static string BookAlreadyOwned => "book_already_owned";
        public static string MovieAlreadyOwned => "movie_already_owned";
        public static string NewspaperAlreadyOwned => "newspaper_already_owned";
        public static string NoAvailableBooks => "no_available_books";
        public static string NoAvailableMovies => "no_available_movies";
        public static string NoAvailableNewspapers => "no_available_newspapers";
        public static string NotOwned => "not_owned";
        public static string InvalidPrice => "invalid_price";
        public static string NotEnoughTickets => "not_enough_tickets";
        public static string InvalidLength => "invalid_length";
        public static string InvalidDirector => "invalid_director";
        public static string TicketAlreadyPurchased => "ticket_already_purchased";
        public static string TicketNotPurchased => "ticket_not_purchased";
        public static string InvalidType => "invalid_type";
    }
}
