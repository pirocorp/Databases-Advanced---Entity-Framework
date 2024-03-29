﻿//Resharper disable InconsistentNaming, CheckNamespace

using System;
using System.Linq;
using System.Reflection;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Theatre;
using Theatre.Data;

[TestFixture]
public class Import_000_001
{
    private IServiceProvider serviceProvider;

    private static readonly Assembly CurrentAssembly = typeof(StartUp).Assembly;

    [SetUp]
    public void Setup()
    {
        Mapper.Reset();
        Mapper.Initialize(cfg => cfg.AddProfile(GetType("TheatreProfile")));

        this.serviceProvider = ConfigureServices<TheatreContext>("Theatre");
    }

    [Test]
    public void ImportPlaysZeroTest()
    {
        var context = this.serviceProvider.GetService<TheatreContext>();

        var inputXml =
            @"<?xml version='1.0' encoding='UTF-8'?>
<Plays>
  <Play>
    <Title>The Hsdfoming</Title>
    <Duration>03:40:00</Duration>
    <Rating>8.2</Rating>
    <Genre>Action</Genre>
    <Description>A guyat Pinter turns into a debatable conundrum as oth ordinary and menacing. Much of this has to do with the fabled Pinter Pause, which simply mirrors the way we often respond to each other in conversation, tossing in remainders of thoughts on one subject well after having moved on to another.</Description>
    <Screenwriter>Roger Nciotti</Screenwriter>
  </Play>
  <Play>
    <Title>Candida</Title>
    <Duration>02:21:00</Duration>
    <Rating>6.5</Rating>
    <Genre>Romance</Genre>
    <Description>What to do about Shaw? So many of his plays zing as comedies and also still work as social commentary. Looking over his canon (pun sort of intended), it struck me that this one of the 'Plays Pleasant' series might be most important.</Description>
    <Screenwriter>Carmina Pollak</Screenwriter>
  </Play>
  <Play>
    <Title>The Hsdfasdng</Title>
    <Duration>03:40:00</Duration>
    <Rating>8.2</Rating>
    <Genre>Horror</Genre>
    <Description>A guyat Pinter turns into a debata Much of this has to do with the fabled Pinter Pause, which simply mirrors the way we often respond to each other in conversation, tossing in remainders of thoughts on one subject well after having moved on to another.</Description>
    <Screenwriter>Roger Ncioasdtti</Screenwriter>
  </Play>
  <Play>
    <Title>The Persianasd</Title>
    <Duration>00:35:00</Duration>
    <Rating>4.1</Rating>
    <Genre>Comedy</Genre>
    <Description></Description>
    <Screenwriter>Fidel Skirlin</Screenwriter>
  </Play>
  <Play>
    <Title>The Perserianasd</Title>
    <Duration>03:35:00</Duration>
    <Rating>41.1</Rating>
    <Genre>Comedy</Genre>
    <Description></Description>
    <Screenwriter>Fidel Skirlin</Screenwriter>
  </Play>
  <Play>
    <Title>Candida</Title>
    <Duration>00:21:00</Duration>
    <Rating>6.5</Rating>
    <Genre>Romance</Genre>
    <Description>What to do about Shaw? So many of his plays zing as comedies and also still work as social commentary. Looking over his canon (pun sort of intended), it struck me that this one of the 'Plays Pleasant' series might be most important.</Description>
    <Screenwriter>Carmina Pollak</Screenwriter>
  </Play>
  <Play>
    <Title></Title>
    <Duration>02:11:00</Duration>
    <Rating>4.1</Rating>
    <Genre>Comedy</Genre>
    <Description>What doesn't this tragedy have? There's sublime poetry, rich psychology for characters of both sexes, , depending on a director's interpretation, a crackling good mystery lying underneath the tale of The Melancholy Dane. Shakespeare took a standard-issue—for the period—genre and used it to create a monument in Western literature, dramatic or non. This play can be debated and dissected ad infinitum.</Description>
    <Screenwriter>Fidel Skirlin</Screenwriter>
  </Play>
  <Play>
    <Title>Playboy of the Western World</Title>
    <Duration>03:21:00</Duration>
    <Rating>6.1</Rating>
    <Genre>Comedy</Genre>
    <Description>The Aristotelian notion that a tragic hero needs to be noble gets thrown out the window in this play about a man who's heralded as a hero for having killed his father in self-defense only to be reviled by those who had cheered him when it turns out the old man was only wounded. The play sparked riots when it premiered in 1907, and while it no longer has the ability to inspire that level of passion, the play is a touchstone for the sort of dark Irish dramas we now expect from the likes of Conor McPherson and Martin McDonagh.</Description>
    <Screenwriter>Loralie Kubec</Screenwriter>
  </Play>
  <Play>
    <Title>The Importance of Being</Title>
    <Duration>04:21:00</Duration>
    <Rating>1.3</Rating>
    <Genre>Musical</Genre>
    <Description>This quintessential comedy of manners has retained its ability to tickle audiences for over 100 years. It's also been an inspiration for numerous writers who have adapted it to suit changing times. Wilde's unparalleled ability to spin cutting epigrams is only one of the reasons that this piece has endured. There's also his genteel mockery of classism and chauvinism. Like the watercress sandwiches that are consumed in the play, it's always a refreshing treat.</Description>
    <Screenwriter>Kaitlin Stapford</Screenwriter>
  </Play>
  <Play>
    <Title>Awake and Sing</Title>
    <Duration>02:41:00</Duration>
    <Rating>3.8</Rating>
    <Genre>Drama</Genre>
    <Description>Tensions run high in this play about three generations of a Bronx Jewish family and each one's pursuit of the American Dream. Can one achieve it while also remaining true to one's heritage? It's a question that immigrants have had to ponder in any decade, and as evidenced by the NAATCO revival in 2015 this kitchen-sink drama poignantly transcends race and religion.</Description>
    <Screenwriter>Catherine Finkle</Screenwriter>
  </Play>
  <Play>
    <Title>The School for Scandal</Title>
    <Duration>01:32:00</Duration>
    <Rating>8.0</Rating>
    <Genre>Musical</Genre>
    <Description>This 18th-century confection skewers the mores and mouths of London's elite as they backstab one another with gossip. It's best served up in an era in which society is itself preying on dirt and innuendo, and for better or worse we're never quite far away from that. Thus, it's a piece that has remained timely and delightful through the centuries.</Description>
    <Screenwriter>Caryl Churchill</Screenwriter>
  </Play>
  <Play>
    <Title>Stuff Happens</Title>
    <Duration>03:10:00</Duration>
    <Rating>6.3</Rating>
    <Genre>Comedy</Genre>
    <Description>Hare borrowed a phrase from Donald Rumsfeld and adopted a Shakespearean flair for both fact and fiction for this play about the events that led up to the Iraq War. Parts of the play are taken verbatim speeches, press conferences and meeting transcripts. Other portions are imagined versions of meetings that took place between elected and other government officials. The result was one of the most impressive political dramas to emerge in recent memory.</Description>
    <Screenwriter>Ardene Blaby</Screenwriter>
  </Play>
  <Play>
    <Title>Life With Father</Title>
    <Duration>04:21:00</Duration>
    <Rating>0</Rating>
    <Genre>Comedy</Genre>
    <Description>Of all the plays on this list, this might be the 'sturdiest' and probably the most old-fashioned. Yet this lithesome comedy about a woman's fears that her husband was never baptized has a unique place in theater annals. Until Fiddler on the Roof came along, Life was the longest-running show in Broadway history, and as late as 1987 it was among the top five long runs. Until a theater is willing to revive it, take a look at the delightful 1947 movie version.</Description>
    <Screenwriter>Geordie Aiskrigg</Screenwriter>
  </Play>
  <Play>
    <Title>Dutchmanfdg</Title>
    <Duration>00:41:44</Duration>
    <Rating>5.1</Rating>
    <Genre>Comedy</Genre>
    <Description></Description>
    <Screenwriter>Fidel Skirlin</Screenwriter>
  </Play>
  <Play>
    <Title>The Normal Heart</Title>
    <Duration>05:21:00</Duration>
    <Rating>7.7</Rating>
    <Genre>Musical</Genre>
    <Description>The energy and anger of a community—and the playwright himself—make this play about the earliest days of the AIDS crisis vibrate with passion and intensity even 30 years after its premiere. Kramer's achievement in this snapshot of events from the early 1980s is twofold: It works as a standalone drama for the ages and retains its edge as a damning piece of political theater from the shameful period in the country's history.</Description>
    <Screenwriter>Nester Enrietto</Screenwriter>
  </Play>
  <Play>
    <Title>Everyman</Title>
    <Duration>02:11:00</Duration>
    <Rating>4.1</Rating>
    <Genre>Drama</Genre>
    <Description></Description>
    <Screenwriter>Fidel Skirlin</Screenwriter>
  </Play>
  <Play>
    <Title>Rosencrantz and Guildenstern Are Dead</Title>
    <Duration>01:34:00</Duration>
    <Rating>8.0</Rating>
    <Genre>Musical</Genre>
    <Description>What happened when Hamlet wasn't at the forefront of events in Elsinore? It's not a question that many would have considered, but leave it to Stoppard's fertile brain to latch onto the question and answer it with a rip-roaring riff on a classic. His ability to mirror the events of his source material and echo its existentialist themes only makes RandG more impressive. It's another debut work whose promise was fulfilled repeatedly over the years, in plays ranging from Arcadia to Jumpers and the elephantine Coast of Utopia trilogy.</Description>
    <Screenwriter>Manda Shorter</Screenwriter>
  </Play>
  <Play>
    <Title></Title>
    <Duration>02:11:44</Duration>
    <Rating>5.1</Rating>
    <Genre>Comedy</Genre>
    <Description>What doesn't this tragedy have? There's sublime poetry, rich psychology for c can be debated and dissected ad infinitum.</Description>
    <Screenwriter>Fidel Skirlin</Screenwriter>
  </Play>
  <Play>
    <Title>This Is Our Youth</Title>
    <Duration>02:15:00</Duration>
    <Rating>8.9</Rating>
    <Genre>Romance</Genre>
    <Description>Lonergan's play about a trio of young people hanging out, squabbling over a coke deal, and looking for some sense of direction in the early years of the Reagan era follows in the footsteps of the British angry young man plays. The fact that it premiered about 10 years after the period in which it's set gave (and gives) this funny or sad piece a haunting resonance for Gen Xers.</Description>
    <Screenwriter>Donaugh Eloy</Screenwriter>
  </Play>
  <Play>
    <Title>Uncommon Women</Title>
    <Duration>02:25:00</Duration>
    <Rating>9.5</Rating>
    <Genre>Romance</Genre>
    <Description>Wasserstein won the Pulitzer for The Heidi Chronicles, but well before that look at life in post-feminist America she wrote this touchingly funny play about a group of Mount Holyoke alums traversing feminism's second wave. As the piece works backward through time from 1978 to 1972, what emerges is a cunning portrait of women during a period when possibilities seemed both infinite and curiously limited.</Description>
    <Screenwriter>Desiree Manley</Screenwriter>
  </Play>
  <Play>
    <Title>What the Butler Saw</Title>
    <Duration>02:25:00</Duration>
    <Rating>5.2</Rating>
    <Genre>Comedy</Genre>
    <Description>With this play Orton takes the British sex farce (and to a lesser extent the British procedural) to the dark side as the insanities of a mental health clinic rise to both bizarre and hilarious heights. Orton tackles everything from sexual and gender politics to governmental ineptitude in this iconoclastic play from the late 1960s that seems particularly apt for revival right now.</Description>
    <Screenwriter>Berny Bertot</Screenwriter>
  </Play>
  <Play>
    <Title>Dutchman</Title>
    <Duration>02:11:44</Duration>
    <Rating>5.1</Rating>
    <Genre>Comedy</Genre>
    <Description></Description>
    <Screenwriter>Fidel Skirlin</Screenwriter>
  </Play>
  
  <Play>
    <Title>Tartuffe</Title>
    <Duration>02:45:00</Duration>
    <Rating>0</Rating>
    <Genre>Drama</Genre>
    <Description>Simultaneously riotous and scathing, this comedy explores and exposes the hypocrisy that can often lie underneath religious fervor and the lengths to which a zealot's followers will go to protect him or her and their beliefs. The play might have been originally written as an indictment of members of Louis XIV's court, but this satire has the ability to speak to almost any age.</Description>
    <Screenwriter>Genvieve Mountfort</Screenwriter>
  </Play>
  <Play>
    <Title>Everymansre</Title>
    <Duration>00:11:00</Duration>
    <Rating>4.1</Rating>
    <Genre>Drama</Genre>
    <Description></Description>
    <Screenwriter>Fidel Skirlin</Screenwriter>
  </Play>
  <Play>
    <Title>The Vortex</Title>
    <Duration>02:50:00</Duration>
    <Rating>9.3</Rating>
    <Genre>Comedy</Genre>
    <Description>Yes, everyone thinks about the frothy farces Private Lives and Blithe Spirit when Sir Noël Coward's name comes up, but here's the one that made his reputation. It's a potboiler of sorts that exposes the extravagance of British youth during the height of the Jazz Age and the privileged Edwardian culture that gave rise to them and their behavior. Drug abuse and a hefty dose of Oedipal love spice up the drama, and all the while Coward's dry wit sparkles.</Description>
    <Screenwriter>Roldan Da Costa</Screenwriter>
  </Play>
  <Play>
    <Title></Title>
    <Duration>02:11:00</Duration>
    <Rating>4.1</Rating>
    <Genre>Drama</Genre>
    <Description>What doesn't t psychology for characters of both sexes, a hefty dose of comedy to leaven the mood, and, depending on a director's interpretation, a crackling good mystery lying underneath the tale of The Melancholy Dane. Shakespeare took a standard-issue—for the period—genre and used it to create a monument in Western literature, dramatic or non. This play can be debated and dissected ad infinitum.</Description>
    <Screenwriter>Fidel Skirlin</Screenwriter>
  </Play>
  <Play>
    <Title>Uncle Vanya</Title>
    <Duration>03:10:00</Duration>
    <Rating>0</Rating>
    <Genre>Drama</Genre>
    <Description>Why Vanya and not The Seagull or Cherry Orchard or Three Sisters, you may ask? Ultimately, for me, this one comes down to scope. All of Chekhov's meticulously observed plays find both the comedy and tragedy in ordinary lives. What sets this one apart from the others is its relative quietness as it looks at small crises in an extended family's everyday existence, becoming something of a benchmark for brooding family drama.</Description>
    <Screenwriter>Russell Crouse</Screenwriter>
  </Play>
  <Play>
    <Title>T</Title>
    <Duration>02:11:00</Duration>
    <Rating>4.1</Rating>
    <Genre>Comedy</Genre>
    <Description></Description>
    <Screenwriter>Fiddel Skighrlin</Screenwriter>
  </Play>
  <Play>
    <Title>Fences</Title>
    <Duration>03:20:00</Duration>
    <Rating>4.7</Rating>
    <Genre>Musical</Genre>
    <Description>Theoretically any of Wilson's 10 plays chronicling the African-American experience in Pittsburgh during the last century would easily fit onto this list, but this one stands apart from the others because of its tremendous heart and its commanding central figure who reaches almost tragic dimensions.</Description>
    <Screenwriter>Carmelia Luckey</Screenwriter>
  </Play>
  <Play>
    <Title>The Pedfgrsianasd</Title>
    <Duration>03:35:00</Duration>
    <Rating>-3.3</Rating>
    <Genre>Comedy</Genre>
    <Description></Description>
    <Screenwriter>Fidel Skirlin</Screenwriter>
  </Play>
  <Play>
    <Title>Machinal</Title>
    <Duration>03:30:00</Duration>
    <Rating>1.4</Rating>
    <Genre>Comedy</Genre>
    <Description>Expressionism and feminism collide in this 1928 play that explores how many women were just disposable objects as the last century dawned. For the heroine of this sometimes-harrowing play, life moves from an office job to marriage to the electric chair with cruel intensity. It's become a mainstay of both the stage and the classroom for good reason.</Description>
    <Screenwriter />
  </Play>
  <Play>
    <Title>The Homecoming</Title>
    <Duration>03:40:00</Duration>
    <Rating>8.2</Rating>
    <Genre>Drama</Genre>
    <Description>A guy brings home his girlfriend to meet the family. It's a simple premise that Pinter turns into a debatable conundrum as he makes action and dialogue concurrently realistic and opaque, both ordinary and menacing. Much of this has to do with the fabled Pinter Pause, which simply mirrors the way we often respond to each other in conversation, tossing in remainders of thoughts on one subject well after having moved on to another.</Description>
    <Screenwriter>Roger Nucciotti</Screenwriter>
  </Play>
  <Play>
    <Title>Hedda Gabler</Title>
    <Duration>03:50:00</Duration>
    <Rating>1.0</Rating>
    <Genre>Musical</Genre>
    <Description>What's a woman in a terrible marriage supposed to do? Norwegian playwright Ibsen gave us a number of answers in his career. With Hedda, the only escape turns out to be suicide. Hedda doesn't strike quite the same feminist blow as another of Ibsen's plays (A Doll's House, where the Nora just leaves), but that's why Hedda is here. This play demonstrates incontrovertibly Ibsen's determination to make his audiences consider feminist issues in the 19th century by presenting them with such a grim outcome.</Description>
    <Screenwriter>Alisun Moon</Screenwriter>
  </Play>
  <Play>
    <Title>The Bald Soprano</Title>
    <Duration>01:10:00</Duration>
    <Rating>3.1</Rating>
    <Genre>Romance</Genre>
    <Description>The life of the complacent bourgeois—and by extension the worlds of many theatergoers—got put through an absurdist prism in this French classic that simultaneously blew the roof off the houses where drawing-room comedies had traditionally taken place. Language, narrative, and character all get zanily and incisively fractured in this play about two couples and the two evenings they spend visiting one another. When the piece debuted in 1950, no one had never seen anything quite like it.</Description>
    <Screenwriter />
  </Play>
  <Play>
    <Title>Waiting for Godot</Title>
    <Duration>01:20:00</Duration>
    <Rating>9.4</Rating>
    <Genre>Comedy</Genre>
    <Description>A new era in playwriting dawned with the debut of this play in 1948. Beckett eschewed standard plot in this piece about two tramps on a mostly barren plain waiting for someone named, obviously, Godot. When he doesn't show in the first act, they do it again with variations in the second. Are they waiting for some sort of perverse God? Is Beckett simply depicting the mundane realities of daily existence in the play? Both? Regardless, Godot brought abstraction center stage and did and still does it beautifully.</Description>
    <Screenwriter>Angelica Cline</Screenwriter>
  </Play>
  <Play>
    <Title>Woyzeck</Title>
    <Duration>01:30:00</Duration>
    <Rating>3.4</Rating>
    <Genre>Drama</Genre>
    <Description>Although this uncompleted script about a soldier's descent into madness was written in the early 19th century, it feels much more like an experimental drama from 100 years later. Part of the reason for this is the fact that it is indeed unfinished and hence sketchy. But Büchner also pioneers objectifying characters by using only their titles to identify them and commandingly sets a standard for dramatizing fever dreams and his central character's fragile grasp on reality.</Description>
    <Screenwriter>Carmencita Cadwallader</Screenwriter>
  </Play>
  <Play>
    <Title>A Raisin in the Sun</Title>
    <Duration>01:40:00</Duration>
    <Rating>5.4</Rating>
    <Genre>Drama</Genre>
    <Description>Hansberry broke a barrier with this drama about an African-American family attempting to better itself by moving to a new neighborhood; she became the first Black woman to have a play produced on Broadway. It's not just this factor that puts Raisin on this list. As we saw with not one but two fine revivals in a period of 10 years, Raisin speaks to audiences of all races and generations because its plot elements and themes cut across ethnic and chronological divides.</Description>
    <Screenwriter>Janaye Kiessel</Screenwriter>
  </Play>
  <Play>
    <Title>Look Back in Anger</Title>
    <Duration>01:50:00</Duration>
    <Rating>5.3</Rating>
    <Genre>Musical</Genre>
    <Description>Wouldn't it be great to write a play that inspired a label for work from an entire generation of writers? This 1956 drama did just that as it took middle age (mostly) out of playwriting and instead offered up a picture of life among a group of discontent British twentysomethings, pulling English drama out of parlors, dining rooms, and genteel patios, and into cramped inner-city apartment squalor. Long live the angry young man play.</Description>
    <Screenwriter>Marlo Dowbekin</Screenwriter>
  </Play>
  <Play>
    <Title>The Glass Menagerie</Title>
    <Duration>01:55:00</Duration>
    <Rating>9.2</Rating>
    <Genre>Drama</Genre>
    <Description>As with so many others on this list, Williams is a playwright whose works could take up several entries. Choosing Menagerie over, say, A Streetcar Named Desire or Cat on a Hot Tin Roof comes down to this: Menagerie is his breakthrough work that introduced his unique brand of theatrical lyricism to the world. And while some of his other plays go farther in terms of stretching stage conventions or tackling weightier issues, this one takes a gentle sliver of a story and turns it into something magical.</Description>
    <Screenwriter>Ranee McConnal</Screenwriter>
  </Play>
  <Play>
    <Title>Th</Title>
    <Duration>02:11:00</Duration>
    <Rating>4.1</Rating>
    <Genre>Comedy</Genre>
    <Description></Description>
    <Screenwriter>Fiddel Skirlin</Screenwriter>
  </Play>
  <Play>
    <Title>Angels in America</Title>
    <Duration>01:12:00</Duration>
    <Rating>9.3</Rating>
    <Genre>Comedy</Genre>
    <Description>Its two parts, Millennium Approaches and Perestroika, give theatergoers a whirlwind trip through stories ranging from a man's battle with AIDS to über-Republican Roy Cohn's homophobia and his own realization that he also has the disease, and from the Rosenbergs' legacy to a Mormon couple's struggle with his gayness and her drug addiction. Digressions include fever dreams and trips to the heavens. It's all exactly what Kushner promises in the piece's subtitle: A Gay Fantasia on National Themes, and the boldness Kushner's storytelling and unbridled imagination means that this one thrills.</Description>
    <Screenwriter>Tony Kushner</Screenwriter>
  </Play>
  <Play>
    <Title>Oedipus Rex</Title>
    <Duration>01:13:00</Duration>
    <Rating>7.2</Rating>
    <Genre>Musical</Genre>
    <Description>Used as the exemplar of dramatic writing in Aristotle's Poetics, this Greek tragedy remains a pillar of playwriting. Before walking into a production or picking up a copy of the script, we all know that King Oedipus has killed his father, married his mother, etc. And yet Sophocles' slow reveal of the truths of the monarch’s life and the pride that sets him and his family spiraling toward a tragic downfall never ceases to be genuinely compelling. This one stands the test of time simply because it's good stage storytelling.</Description>
    <Screenwriter>Merna Menham</Screenwriter>
  </Play>
  <Play>
    <Title>Death of a Salesman</Title>
    <Duration>02:16:00</Duration>
    <Rating>9.9</Rating>
    <Genre>Romance</Genre>
    <Description>Not just to Willy Loman and the sad realities of his life as a mediocre traveling salesman and the delusions that barely keep him afloat, but also to Miller's exquisite modern tragedy about an average Joe. Both grittily naturalistic and ethereally dream-like, this one punches the audience in the gut time and again simply because it allows us to witness his heartbreaking final downfall while also allowing us to go inside his mind to seemingly feel his deep-seated pain.</Description>
    <Screenwriter>Lucina Braznell</Screenwriter>
  </Play>
  <Play>
    <Title>The Glass Menagerie</Title>
    <Duration>02:17:00</Duration>
    <Rating>9.0</Rating>
    <Genre>Romance</Genre>
    <Description>As with so many others on this list, Williams is a playwright whose works could take up several entries. Choosing Menagerie over, say, A Streetcar Named Desire or Cat on a Hot Tin Roof comes down to this: Menagerie is his breakthrough work that introduced his unique brand of theatrical lyricism to the world. And while some of his other plays go farther in terms of stretching stage conventions or tackling weightier issues, this one takes a gentle sliver of a story and turns it into something magical.</Description>
    <Screenwriter>Jeanna Worboys</Screenwriter>
  </Play>
  <Play>
    <Title>The Persian</Title>
    <Duration>02:11:00</Duration>
    <Rating>4.1</Rating>
    <Genre>Comedy</Genre>
    <Description></Description>
    <Screenwriter>Fidel Skirlin</Screenwriter>
  </Play>
  <Play>
    <Title>Hamlet</Title>
    <Duration>02:11:00</Duration>
    <Rating>4.1</Rating>
    <Genre>Comedy</Genre>
    <Description>What doesn't this tragedy have? There's sublime poetry, rich psychology for characters of both sexes, a hefty dose of comedy to leaven the mood, and, depending on a director's interpretation, a crackling good mystery lying underneath the tale of The Melancholy Dane. Shakespeare took a standard-issue—for the period—genre and used it to create a monument in Western literature, dramatic or non. This play can be debated and dissected ad infinitum.</Description>
    <Screenwriter>Fidel Skirlin</Screenwriter>
  </Play>
  <Play>
    <Title>Three Kingdoms</Title>
    <Duration>02:55:00</Duration>
    <Rating>6.1</Rating>
    <Genre>Drama</Genre>
    <Description>Few productions this century have divided opinion like Three Kingdoms. Simon Stephens’ detective yarn took audiences on a thrilling and disorientating trip across Europe, in a trilingual collaboration with German director Sebastian Nübling and Estonian designer Ene-Liis Semper. Loathed and loved in equal measure, it has undoubtedly galvanised a new generation of directors inspired by continental European theatre. And in its pre-Brexit deconstruction of Britain’s relationship with Europe, it feels chillingly prescient.</Description>
    <Screenwriter>Sebastian Nubling</Screenwriter>
  </Play>
  <Play>
    <Title>Mission Drift</Title>
    <Duration>03:21:00</Duration>
    <Rating>7.1</Rating>
    <Genre>Comedy</Genre>
    <Description>Brilliantly collapsing time and space, the Team’s dissection of US capitalism is one of the most theatrically ambitious shows of recent years. Mission Drift’s heady mix of history, mythology and floor-shaking tunes skewered the American dream while recreating the giddy, greedy thrill it promises. In the age of Donald Trump, I’ve yet to see another play that quite so incisively captures the myth of the United States and all the ugliness it has bred</Description>
    <Screenwriter>John Smith</Screenwriter>
  </Play>
</Plays>";
           
        ;
        var actualOutput =
            Theatre.DataProcessor.Deserializer.ImportPlays(context, inputXml).TrimEnd();
        ;
        var expectedOutput =
            "Invalid data!\r\nSuccessfully imported Candida with genre Romance and a rating of 6.5!\r\nInvalid data!\r\nInvalid data!\r\nInvalid data!\r\nInvalid data!\r\nInvalid data!\r\nSuccessfully imported Playboy of the Western World with genre Comedy and a rating of 6.1!\r\nSuccessfully imported The Importance of Being with genre Musical and a rating of 1.3!\r\nSuccessfully imported Awake and Sing with genre Drama and a rating of 3.8!\r\nSuccessfully imported The School for Scandal with genre Musical and a rating of 8!\r\nSuccessfully imported Stuff Happens with genre Comedy and a rating of 6.3!\r\nSuccessfully imported Life With Father with genre Comedy and a rating of 0!\r\nInvalid data!\r\nSuccessfully imported The Normal Heart with genre Musical and a rating of 7.7!\r\nInvalid data!\r\nSuccessfully imported Rosencrantz and Guildenstern Are Dead with genre Musical and a rating of 8!\r\nInvalid data!\r\nSuccessfully imported This Is Our Youth with genre Romance and a rating of 8.9!\r\nSuccessfully imported Uncommon Women with genre Romance and a rating of 9.5!\r\nSuccessfully imported What the Butler Saw with genre Comedy and a rating of 5.2!\r\nInvalid data!\r\nSuccessfully imported Tartuffe with genre Drama and a rating of 0!\r\nInvalid data!\r\nSuccessfully imported The Vortex with genre Comedy and a rating of 9.3!\r\nInvalid data!\r\nSuccessfully imported Uncle Vanya with genre Drama and a rating of 0!\r\nInvalid data!\r\nSuccessfully imported Fences with genre Musical and a rating of 4.7!\r\nInvalid data!\r\nInvalid data!\r\nSuccessfully imported The Homecoming with genre Drama and a rating of 8.2!\r\nSuccessfully imported Hedda Gabler with genre Musical and a rating of 1!\r\nInvalid data!\r\nSuccessfully imported Waiting for Godot with genre Comedy and a rating of 9.4!\r\nSuccessfully imported Woyzeck with genre Drama and a rating of 3.4!\r\nSuccessfully imported A Raisin in the Sun with genre Drama and a rating of 5.4!\r\nSuccessfully imported Look Back in Anger with genre Musical and a rating of 5.3!\r\nSuccessfully imported The Glass Menagerie with genre Drama and a rating of 9.2!\r\nInvalid data!\r\nSuccessfully imported Angels in America with genre Comedy and a rating of 9.3!\r\nSuccessfully imported Oedipus Rex with genre Musical and a rating of 7.2!\r\nSuccessfully imported Death of a Salesman with genre Romance and a rating of 9.9!\r\nSuccessfully imported The Glass Menagerie with genre Romance and a rating of 9!\r\nInvalid data!\r\nSuccessfully imported Hamlet with genre Comedy and a rating of 4.1!\r\nSuccessfully imported Three Kingdoms with genre Drama and a rating of 6.1!\r\nSuccessfully imported Mission Drift with genre Comedy and a rating of 7.1!";

        var assertContext = this.serviceProvider.GetService<TheatreContext>();

        const int expectedTheaterCount = 30;
        var actualMovieCount = assertContext.Plays.Count();

        Assert.That(actualMovieCount, Is.EqualTo(expectedTheaterCount),
            $"Inserted {nameof(context.Plays)} count is incorrect!");

        Assert.That(actualOutput, Is.EqualTo(expectedOutput).NoClip,
            $"{nameof(Theatre.DataProcessor.Deserializer.ImportPlays)} output is incorrect!");
    }

    private static Type GetType(string modelName)
    {
        var modelType = CurrentAssembly
            .GetTypes()
            .FirstOrDefault(t => t.Name == modelName);

        Assert.IsNotNull(modelType, $"{modelName} model not found!");

        return modelType;
    }

    private static IServiceProvider ConfigureServices<TContext>(string databaseName)
        where TContext : DbContext
    {
        var services = ConfigureDbContext<TContext>(databaseName);

        var context = services.GetService<TContext>();

        try
        {
            context.Model.GetEntityTypes();
        }
        catch (InvalidOperationException ex) when (ex.Source == "Microsoft.EntityFrameworkCore.Proxies")
        {
            services = ConfigureDbContext<TContext>(databaseName, useLazyLoading: true);
        }

        return services;
    }

    private static IServiceProvider ConfigureDbContext<TContext>(string databaseName, bool useLazyLoading = false)
        where TContext : DbContext
    {
        var services = new ServiceCollection()
          .AddDbContext<TContext>(t => t
          .UseInMemoryDatabase(Guid.NewGuid().ToString())
          );

        var serviceProvider = services.BuildServiceProvider();
        return serviceProvider;
    }
}