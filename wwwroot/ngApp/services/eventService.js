class EventService {
    constructor() {
        let eventList = [
            {
                message: "You were mugged at knifepoint by Gwenvel!",
                message2: "lose",
                win: -1,
                km: 0,
                trips: 0,
                image: "/images/gwenvel.gif"
            },
            {
                message: "You run into Gwenvel but manage to get away before she notices you...",
                message2: "lose",
                win: 1,
                km: 0,
                trips: 0,
                image: "/images/gwenvel.gif"
            },
            {
                message: "Gwenvel was going to mug you but you give her some mints. She is very happy and gives you a gift.",
                message2: "are given",
                win: 1,
                km: 0,
                trips: 0,
                image: "/images/happydragon.png"
            },
            {
                message: "You eat some wild mushrooms and get very, very lost. However, you gain a trip!",
                message2: "gain",
                win: 1,
                km: 0,
                trips: 1,
                image: "/images/happydragon.png"
            },
            {
                message: "You get very drunk and decide to take to the sky! Unfortunately, you get arrested for F.W.I. - flying while intoxicated. You lose some KM traveled and a trip.",
                message2: "also lose",
                win: -1,
                km: -.2,
                trips: -1,
                image: "/images/saddragon.png"
            },
            {
                message: "Your rich uncle, who happens to be an actual Nigerian Prince, dies leaving you a fortune!",
                message2: "inherit",
                win: 1,
                km: 0,
                trips: 0,
                image: "/images/happydragon.png"
            },
            {
                message: "You get busted doing some 'hoodrat s**t' by your grandma and the police. You lose some KM traveled.",
                message2: "also lose",
                win: -1,
                km: -.1,
                trips: 0,
                image: "/images/saddragon.png"
            },
            {
                message: "You get sued for child support!",
                message2: "are charged",
                win: -1,
                km: 0,
                trips: 0,
                image: "/images/saddragon.png"
            },
            {
                message: "You've been living the good wander life but your doctor says you need to 'go on a diet!'",
                message2: "lose",
                win: -1,
                km: 0,
                trips: 0,
                image: "/images/saddragon.png"
            },
            {
                message: "You forget to charge your iPhone and without GPS, you get lost...",
                message2: "end up losing",
                win: -1,
                km: 0,
                trips: 0,
                image: "/images/saddragon.png"
            }
        ];
        this.eventList = eventList;
    }
    getEvent(number) {
        return this.eventList[number];
    }
}