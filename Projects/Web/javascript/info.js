//////////////////////////////////////////////// about Profile ///////////////////////////////////////////

var info_login = {
    grant_type: "password", // it needs to be this, don't change
    Username: "",
    Password: ""
}

var info_profile_register = {
    User: {
        Email: "" // the email address
    },
    Name: "", // the name
}

var info_profile_single = {
    ID: "" // the id, xxxx-xxxx-xxxx-xxxxxxx format
}

var info_profile_multiple = null // passing a null will be fine

var info_profile_single_email = {
    User: {
        Email: "" // the email
    }
}

var info_profile_assign_apartment = {
    ID: "", // the profile id
    Apartments: [
        {
            ID: "" // the apartment id
        }
    ]
}

var info_profile_remove_apartment = {
    ID: "", // the profile id
    Apartments: [
        {
            ID: "" // the apartment id
        }
    ]
}

var info_profile_assign_building = {
    ID: "", // the profile id
    Buildings: [
        {
            ID: "" // the building id
        }
    ]
}

var info_profile_remove_building = {
    ID: "", // the profile id
    Buildings: [
        {
            ID: "" // the building id
        }
    ]
}

//////////////////////////////////////////////// about Building ///////////////////////////////////////////

var info_building_create = {
    Name: "",
    Address: "",
    Superintendents: [
        {
            ID: "" // The Profile id of who is creating the building, will be automatically assign as superintendent
        }
    ]
}

var info_building_multiple = null; // Passing a null will be fine

var info_building_single = {
    ID: "" // the building id
}

//////////////////////////////////////////////// about Apartment ///////////////////////////////////////////

var info_apartment_create = {
    Number: 123, // The apartment number
    BuildingID: "" // The building id that this apartment belongs to
}

var info_apartment_multiple = null;

var info_apartment_single = {
    ID: "" // the apartment id of the apartment you want to get
}

//////////////////////////////////////////////// about Request ///////////////////////////////////////////

var info_request_create = {

    Category: 0, //The category index of this request, about the index you can check Project Folder/Api/Models/Type.cs for the number
    Sub: 0, //The same, the sub category number
    ApartmentID: "",//The apartment id this request is from
    RequestTenantID: "" //the profile id of the person this request is from

}

var info_request_response = {

    ID: "", //the request id you're responsing to
    ResponseSuperintenentID: "" // The responsing superintenent id

}

var info_request_done = {

    ID: "", //the request id you're trying to finish

}

var info_request_multiple = null; //the same, passing a null to get all the records

var info_request_multiple_profile = {
    ID: "",//the profile id you're getting requests records with
}

var info_request_single = {
    ID: "",// the request id
}