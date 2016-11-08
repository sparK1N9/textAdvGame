using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

public class textadv : MonoBehaviour
{
    public string currentRoom;
    public string text;
    public string north;
    public string west;
    public string east;
    public string south;
    public AudioSource sfxSource;
    public AudioSource sfxSource2;
    public AudioClip winSound;
    public AudioClip doorSound;
    public AudioClip keySound;
    public Camera cam;
    public RawImage illustration;
    List<string> story = new List<string>();
    public List<string> inventory = new List<string>();
    int counter = 0;
    int i = 0;
    int locked = 1;
    int s = 0;
    // Use this for initialization
    void Start()
    {
        currentRoom = "title";
        story.Add("EX-Nyarlathotep crawled out of nowhere and is causing chaos!\nLuckily, our hero, lord, and savior, Gabe Boomer, is here to save the day!\nIt is going to be a battle like no other, but right now it is your chance to make your moves!\nWhat you need to do is …\n\nPress Enter to continue");
        story.Add("Break into his mansion and steal some treasures before he comes back.\n......\nWell, after all, you are just a normal poor human being that has no power to fight monsters. On top of that, your family is starving and you need to find money for food, ASAP!\nOf course, you wanted to steal from the evil people who ruins others’ lives for their own profit, instead of stealing from a hero. But not surprising, those people are the ones with the most guarded mansions, and it is not worth it to risk your life over there. “At least Gabe is really rich so he probably won’t notice what’s missing anyway.” You say that to yourself to make yourself feel better.\n\nPress Enter to continue");
        story.Add("TL;DR: Find the treasure, then GTFO.\n\nNow press that enter key.");
        sfxSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        north = "nil";
        south = "nil";
        west = "nil";
        east = "nil";
        if (currentRoom == "title") illustration.enabled = true;
        else illustration.enabled = false;
        if (currentRoom == "title")
        {
            i = 0;
            text = "Crawling\n\nBy Haotian Shen\n\nPress Enter to begin";
            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter)) currentRoom = "entryway";
        }
        else if (currentRoom == "entryway")
        {
            i = 0;
            if (counter < 3)
            {
                text = story[counter];
                if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter)) counter++;
            }
            else
            {
                text = "You are at the entryway. The living room is right ahead.";
                north = "living room";
                i = 1;
            }
        }
        else if (currentRoom == "living room")
        {
            text = "Looks like a normal living room with nothing special.";
            i = 1;
            south = "entryway";
            west = "bathroom";
            east = "closet";
            north = "kitchen";
        }
        else if (currentRoom == "bathroom")
        {
            text = "???\nYou think Gabe will hide his treasures in his bathroom???\nU WOT M8?";
            i = 1;
            east = "living room";
        }
        else if (currentRoom == "closet")
        {
            text = "You find a lot of clothes and armors that are too huge for you. These armors look pretty expensive, but probably too heavy to steal.";
            i = 1;
            west = "living room";
        }
        else if (currentRoom == "kitchen")
        {
            text = "You find a small piece of paper that reads:\"There is a cake at the end of the game\". You have no idea what that means.";
            i = 1;
            south = "living room";
            west = "storage";
            east = "library";
            north = "bedroom";
        }
        else if (currentRoom == "storage")
        {
            text = "This seems to be where gabe stores his arsenal. You also notice that there are a lot of clocks that are off by a lot.";
            i = 1;
            if (!inventory.Contains("keys"))
            {
                text += "\nAfter a more thorough search, you find 2 keys in a drawer.\n\nPress 1 to take all the keys\nPress 2 to only take two keys.\nPress 4 to only take the keys that are in the drawer.";
                if (Input.GetKeyDown(KeyCode.Keypad1) || Input.GetKeyDown(KeyCode.Keypad2) || Input.GetKeyDown(KeyCode.Keypad4)) {
                    inventory.Add("keys");
                    sfxSource2.clip = keySound;
                    sfxSource.Pause();
                    if (!sfxSource2.isPlaying) sfxSource2.Play();
                    sfxSource.Play();
                }
            }
            east = "kitchen";
        }
        else if (currentRoom == "library")
        {
            text = "You find an orange box labeled \"HL3\". You also find another piece of paper that contains the following information:\n\"Game Maker: Haotian Shen, Male, 09/11/1996, New York University, CS Major.\"\nYou are not sure what all this is about.";
            i = 1;
            west = "kitchen";
        }
        else if (currentRoom == "bedroom")
        {
            s = 0;
            text = "You see a big protrait of Gabe on the wall and he is staring at you.";
            i = 1;
            south = "kitchen";
            east = "mystical room";
            if (locked == 0) north = "hidden room";
            else
            {
                text += "\nUpon closer inspect, you find a locked door behind the protrait.";
                if (inventory.Contains("keys")) text += "\n\nPress 0 to unlock the door with your keys";
                if (Input.GetKeyDown(KeyCode.Keypad0) && inventory.Contains("keys")) {
                    locked = 0;
                    sfxSource2.clip = doorSound;
                    sfxSource.Pause();
                    if (!sfxSource2.isPlaying) sfxSource2.Play();
                    sfxSource.Play();
                }
            }
        }
        else if (currentRoom == "mystical room")
        {
            text = "You see weird combinations of numbers and symbols, such as \"-75%\" and \"-60%\", all over the place. You have a feeling that if you stays here just a little longer, you will be poorer than before you came here.";
            i = 1;
            west = "bedroom";
        }
        else if (currentRoom == "hidden room")
        {
            text = "There is a crate. You can almost be certain that the treasures you are looking for are inside the crate.\nThe lock requires a 4 digit password. Right next to the lock, there is a line that reads: Game Maker's birthday(mmdd)\n\n*What you entered will not be on the screen. You will know when you got it right.";
            i = 1;
            south = "bedroom";
            if ((Input.GetKeyDown(KeyCode.Keypad0) && s == 0) || (Input.GetKeyDown(KeyCode.Keypad9) && s == 1) || (Input.GetKeyDown(KeyCode.Keypad1) && s == 3) || (Input.GetKeyDown(KeyCode.Keypad1) && s == 2)) s++;
            else if (s == 4)
            {
                sfxSource.Pause();
                sfxSource2.clip = winSound;
                if (!sfxSource2.isPlaying) sfxSource2.Play();
                text = "You opened the crate and found a rare item. You returned the keys and left with the rare item. You did not get caught, but you have a feeling that one day Gabe will make you pay. (Maybe during the winter sale?)\nBTW, yes, that cake is a lie :P";
                i = 0;
            }
        }

        if (i == 1)
        {
            text += "\n\n";
            if (north != "nil")
            {
                text += "Press ↑ to go to the " + north + "\n";
                if (Input.GetKeyDown(KeyCode.UpArrow)) currentRoom = north;
            }
            if (south != "nil")
            {
                text += "Press ↓ to go to the " + south + "\n";
                if (Input.GetKeyDown(KeyCode.DownArrow)) currentRoom = south;
            }
            if (east != "nil")
            {
                text += "Press → to go to the " + east + "\n";
                if (Input.GetKeyDown(KeyCode.RightArrow)) currentRoom = east;
            }

            if (west != "nil")
            {
                text += "Press ← to go to the " + west + "\n";
                if (Input.GetKeyDown(KeyCode.LeftArrow)) currentRoom = west;
            }
        }
        GetComponent<Text>().text = text;
    }
}
