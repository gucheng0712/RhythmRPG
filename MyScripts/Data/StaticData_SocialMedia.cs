using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticData_SocialMedia
{
    public string[] groupyNamesArr;
    public Dictionary<string, List<string>> tweetsData;
    public Dictionary<string, List<string>> groupyTweetsData = new Dictionary<string, List<string>>();

    public StaticData_SocialMedia()
    {
        InitializeGroupyNameListData();
        //InitializePlayerTweetsData();
        InitializeGroupyTweetsData();
    }


    void InitializeGroupyNameListData()
    {
        groupyNamesArr = new string[]
        {
            "Avaator",
            "Ogz",
            "Rubber",
            "Andromeda",
            "Tracker",
            "Tinker",
            "Abah",
            "Boomer",
            "Ibftron",
            "Epbx",
            "Bit",
            "Gearz",
            "Agextron",
            "Oropx",
            "Aqbtron",
            "Cybel"
        };
    }

    void InitializePlayerTweetsData()
    {
        tweetsData = new Dictionary<string, List<string>>()
        {
            {
                "bar", new List<string>()
                {
                    "This is my first show on the road to fame - come out and support me tonight at the bar!",
                    "Playing an original song tonight at the bar, it’s going to be great.",
                    "Come out to the bar... going to show this town what I can do.",
                    "This world is going to see a new wave of musicians! Come to the bar tonight to see it happen.",
                    "The bar is my first step to fame, come out and support me there."
                }
            },

            {
                "restaurant", new List<string>()
                {
                    "They didn’t want me playing at the restaurant tonight, but with your help, I’m in! Come show your support!",
                    "New song tonight! Come to the restaurant to hear it.",
                    "Need your support tonight at the restaurant.",
                    "Fame and recognition are what I want, and the restaurant is the next stop. Come out tonight and support me!",
                    "Come for the food, stay for the music. I’m playing at the restaurant tonight!"
                }
            },

            {
                "nightClub", new List<string>()
                {
                    "Finally got into the nightclub! Tonight let's show this town what I’m made of.",
                    "Hitting the nightclub tonight, it’s going to go down. Come out and let’s party.",
                    "Performing a new song tonight for the club, come check it out.",
                    "Let’s pack this club tonight and show everyone that I’m the best!",
                    "Going to be the best performer!! Come to the club tonight and let’s make this dream into a reality."
                }
            },

            {
                "musicFestival", new List<string>()
                {
                    "Playing at the Festival tonight! Finally made it time, to show everyone what I got.",
                    "I’m headlining the festival tonight, going to get the recognition that I deserve.",
                    "I couldn’t have made it to the festival without you guys - tonight is going to get crazy.",
                    "My road to fame is almost at its end... come out to the festival tonight!",
                    "I outdid myself on this new song! Come to the festival to hear it. Tonight is going to be legendary."
                }
            }
        };
    }


    void InitializeGroupyTweetsData()
    {
        groupyTweetsData = new Dictionary<string, List<string>>()
        {
            {
                "tweetDialog", new List<string>()
                {
                    "Looking forward to it!!!!",
                    "Omg yessss can’t wait",
                    "Are you coming there any time soon?",
                    "Come to my house",
                    "Need tickets for your show at the festival",
                    "01001111 01101101 01100111 00100000 01111001 01100101 01110011 01110011 01110011 01110011 00100000 01100011 01100001 01101110 01110100 00100000 01110111 01100001 01101001 01110100",
                    "tickets are sold out",
                    "I'm over the moon that you'll be playing there. It is such a magical venue.",
                    "Can’t miss it like this is a must",
                    "Yes yes yes yes",
                    "So excited to finally see you on stage",
                    "So excited that you are going back to this stage. Missed you first time around there.",
                    "See you soon!",
                    "can't wait to see you there! x_x",
                    "Finally!I can't wait to see you performing your music!",
                    "01100110 01110101 01110101 01110101 01110101 01110101 01110101 01000011 01101011 00100001 00100000 01100011 01100001 01101110 00011001 01110100 00100000 01110111 01100001 01101001 01110100 00100001",
                    "Yesss this is my first concert, I can't wait to go-!",
                    "Yes!Looking forward to the show. You killed it last time",
                    "I’m so freakin’ excited!",
                    "i’m going i’m so fucking excited",
                    "so pumped for your show",
                    "WE'LL SEE U TONIGHT!!! I'M YELLING CUZ I'M EXCITED AND HAD A GREAT DAY! Ps can't wait for the show",
                    "Absolutely can’t wait to see you and talk to you, you kind soul",
                    "I wish I could make it but I’m going to be in boot camp ; -;",
                    "God dam I may be ready for another show tonight",
                    "See you there",
                    "YOU are number ONE!!",
                    "NOTICE ME SEMPAI",
                    "Gonna turn up TONIGHT!!!",
                    "Who’s buying drinks tonight ?",
                    "Anyone got extra tickets ?",
                    "Need a ride anyone got room ?",
                    "Selling tickets 50, 000 pesos",
                    "Can I get a few tickets ?",
                    "Any ladies going ?",
                    "Going out with the BOIS!",
                    "YEAAAAH MUSIC!!!!",
                    "I need the VIBES!",
                    "Meet you there!",
                    "Cuties come along",
                    "What’s the exchange rate for pesos? I’m short.",
                    "Someone take me with them please."
                }
            },

            {
                "goodResponse", new List<string>()
                {
                    "Last night was great you killed it",
                    "omg the show at [venue] was so good",
                    "Loved seeing you perform last night",
                    "I couldn’t stop rocking out to your music",
                    "You made my birthday ten times better",
                    "Can’t wait to see you perform next time",
                    "You couldn’t have done any better last night you were the best",
                    "Last night couldn’t have gone any better",
                    "01001001 00100000 01101000 01100001 01100100 00100000 01101110 01101111 01110100 01101000 01101001 01101110 01100111 00100000 01100101 01101100 01110011 01100101",
                    "01000100 01101001 01100100 00100000 01111001 01101111 01110101 00100000 01100100 01100101 01100011 01101111 01100100 01100101 00100000 01110100 01101000 01101001 01110011",
                    "Best night of my life so far.",
                    "When is the next show?",
                    "Lots of hotties, going again FO SHO",
                    "The ladies loved ya! <3",
                    "Don’t remember much last night, maybe it was that good",
                    "Snuck out of the house for a killer performance",
                    "WORTH!",
                    "B0ss I habe to go again pls",
                    "BOI KILLED IT",
                    "This guy is gonna blow UP!",
                    "Brooooooo when is the world tour ?",
                    "My girl and I loved your performance.",
                    "Come back again!",
                    "My boyfriend wants to take me next time!",
                    "V I B E S.",
                    "Worth the pesos.",
                    "My mom caught me at the concert, she stayed till the end",
                    "Spending my paycheck on this was worth it",
                    "Much < 3 bby",
                    "Dope…",
                    "I have work tomorrow but * *ck it!AWESOME SHOW!!"
                }
            },

            {
                "badResponse", new List<string>()
                {
                    "I seen better performances at the (venue )",
                    "I thought you would have been better",
                    "You sucked last night",
                    "You ruined my night",
                    "What were you thinking last night",
                    "I’m no longer a fan of your music",
                    "Was that even music that you were playing last night",
                    "I lost all hope for you",
                    "01001001 00100000 01101000 01101111 01110000 01100101 00100000 01110100 01101000 01101001 01110011 00100000 01100111 01100001 01101101 01100101 00100000 01101001 01110011 00100000 01100111 01101111 01101111 01100100",
                    "01001001 00100000 01101100 01101111 01110110 01100101 00100000 01110000 01101001 01100101",
                    "Wtf happened ?",
                    "The booze was better than the music",
                    "I paid M O N E Y for this ? !?",
                    "*@#3 suck go #$!@ you #$5@@0",
                    "I mean there is always next time ?",
                    "Don’t show up here again",
                    "I couldn’t make a move last night because of you",
                    "The show was something for old people",
                    "My ears were blown to how bad it was",
                    "Not worth hella bread and 17 cent",
                    "I was grounded after this show, not worth",
                    "It’s okay you did your best ?",
                    "It was a cute performance",
                    "Who tf ACTUALLY enjoyed this ?",
                    "It was like playing for a funeral. DEAD!",
                    "Some poor person paid pesos for this lmao.FeelsBadMan",
                    "ResidentSleeper zzzzzzzzzzzzz",
                    "Whelp there’s money not going to my tuition",
                    "The wife can’t find out about this loss of money",
                    "The husband can’t find out about this loss of money",
                    "Total waste of pesos"
                }
            }
        };
    }

}
