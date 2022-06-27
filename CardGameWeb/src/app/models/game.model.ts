import { Card } from "./card.model";
import { Player } from "./player.model";

export class Game {
    id?: number;
    cards?: Card[];
    players?: Player[];
}