-> start

=== start ===
+ What's that sound?
    - It's an alarm clock.

+ Why is it ringing in my ears?
    -> whyRinging
+ Ah right, time to wake up
    - I have to go to work.
    -> dontWannaWork

=== whyRinging ===
Its job is to wake you up.
-> knowRinging

=== knowRinging ===
+ Why is it waking me up?
    -> whyWake
+ I have to go to work.
    -> dontWannaWork
    
=== whyWake ===
You have to go to work.
+ What's work?
    - You have to go to an office to write stuff all day.
That's your work.
-> dontWannaWork

=== dontWannaWork ===
+ I don't wanna go to work. 
    ++ What can I do?
        - This is how the world works. You MUST work.
+ Can I change how the world works?
    -> wannaChange
+ I don't wanna go to work tho. Can I at least scream?
    - Yes you can. Everyone does.
    -> wannaScream

=== wannaChange ===

Maybe.
+ How can I do it?
    - There is this obscure practice called "politics".
But I don't think you have what it takes.
+ Yeah I guess so.
    ++ I don't wanna go to work tho. Can I at least scream?
        - Yes you can. Everyone does.
-> wannaScream

=== wannaScream ===


+ I DON'T WANNA GO TO WORK. I DON'T WANNA GO TO WORK. I DON'T WANNA GO TO WORK.
    ++ I have to work every day. Then I have to work every tomorrow.
        +++ IT'S LIKE A TIMELOOP.
            - you're right. It IS a loop.
+ I wanna get out of it. I would die to get out of it.
    - I don't think dying can help.
+ It can. 
    ++ It has to.
        +++ Dying is the way out.
            ++++ Dying IS the way out. 
                +++++ Dying-
                    - Alright, time to wake up.
-> END