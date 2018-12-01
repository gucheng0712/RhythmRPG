using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum VenuePreference {
    Ambient,
    Experimental,
    Hip,
    Loud,
    None
};

public class VenueProperties : MonoBehaviour {

    [Header("Venue Music Preferences")]
    [Tooltip("The type of music that the venue likes.")]
    public VenuePreference venuePreference;
    [Tooltip("The type of music that the venue dislikes.")]
    public VenuePreference venueDislike;

}
