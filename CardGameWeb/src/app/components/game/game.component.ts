import { Component, OnInit } from '@angular/core';
import { Card } from 'src/app/models/card.model';
import { Game } from 'src/app/models/game.model';
import { Player } from 'src/app/models/player.model';
import { GameService } from 'src/app/services/game.service';

@Component({
    selector: 'app-game',
    templateUrl: './game.component.html',
    styleUrls: ['./game.component.css']
})
export class GameComponent implements OnInit {

    game: Game = {
        id: 0,
        cards: []
    };

    orderedPlayers:Player[] = [];
    undealtCardsBySuit:string = "No cards";
    undealtCards:string = "No cards";

    gameIdToLoad?: number;
    playerToAdd?:string;
    playerToRemove?:string;
    playerToDeal?:string;
    cardsToDeal?:number;

    actionResult: string = "Choose an action";

    constructor(private gameService: GameService) { }

    ngOnInit(): void {
    }

    refreshGame(): void {

        this.gameService.getGame(this.game.id)
            .subscribe({
                next: (res) => {
                    this.game = res;
                    this.refreshPlayers();
                    this.refreshUndealtCards();
                    this.refreshUndealtCardsBySuit();
                },
                error: (e) => {
                    this.game = {
                        id: 0,
                        cards: []
                    };
                    this.actionResult = e.error;
                }
            });
    }

    refreshUndealtCards(): void {

        this.gameService.getUndealt(this.game.id)
            .subscribe({
                next: (res) => {
                    this.undealtCards = res.totals;
                },
                error: (e) => {
                    this.undealtCards = "No cards";
                    this.actionResult = e.error;
                }
            });
    }

    refreshUndealtCardsBySuit(): void {

        this.gameService.getUndealtBySuit(this.game.id)
            .subscribe({
                next: (res) => {
                    this.undealtCardsBySuit = res.totals;
                },
                error: (e) => {
                    this.undealtCardsBySuit = "No cards";
                    this.actionResult = e.error;
                }
            });
    }

    refreshPlayers(): void {

        this.gameService.getPlayers(this.game.id)
            .subscribe({
                next: (res) => {
                    this.orderedPlayers = res;
                },
                error: (e) => {
                    this.orderedPlayers = [];
                    this.actionResult = e.error;
                }
            });
    }

    load(): void {
        this.game.id = this.gameIdToLoad;
        this.actionResult = `Loaded game ${this.game.id}`;
        this.refreshGame();
    }

    create(): void {

        this.gameService.start()
            .subscribe({
                next: (res) => {
                    this.game = res;
                    this.orderedPlayers = [];
                    this.undealtCards = "No cards";
                    this.undealtCardsBySuit = "No cards";
                    this.actionResult = `Game ${this.game.id} created`;
                },
                error: (e) => {
                    this.actionResult = e.error;
                }
            });

    }

    end(): void {

        let id = this.game.id;
        this.gameService.endGame(this.game.id)
            .subscribe({
                next: (res) => {
                    this.actionResult = `Game ${id} ended`;
                    this.game = {
                        id: 0,
                        cards: []
                    };
                },
                error: (e) => {
                    this.actionResult = e.error;
                }
            });

    }

    addDeck(): void {

        this.gameService.addDeck(this.game.id)
            .subscribe({
                next: (res) => {
                    this.actionResult = `Deck added`;
                    this.refreshGame();
                },
                error: (e) => {
                    this.actionResult = e.error;
                }
            });
        
    }

    addPlayer(): void {

        this.gameService.addPlayer(this.game.id, this.playerToAdd)
            .subscribe({
                next: (res) => {
                    this.actionResult = `Player ${this.playerToAdd} added`;
                    this.refreshGame();
                },
                error: (e) => {
                    this.actionResult = e.error;
                }
            });
        
    }

    removePlayer(): void {

        this.gameService.removePlayer(this.game.id, this.playerToRemove)
            .subscribe({
                next: (res) => {
                    this.actionResult = `Player ${this.playerToRemove} removed`;
                    this.refreshGame();
                },
                error: (e) => {
                    this.actionResult = e.error;
                }
            });

    }

    shuffle(): void {
        this.gameService.shuffle(this.game.id)
            .subscribe({
                next: (res) => {
                    this.actionResult = `Cards shuffled`;
                    this.refreshGame();
                },
                error: (e) => {
                    this.actionResult = e.error;
                }
            });
    }

    deal(): void {
        this.gameService.deal(this.game.id, this.playerToDeal, this.cardsToDeal)
            .subscribe({
                next: (res) => {
                    this.actionResult = `${this.cardsToDeal} dealt to ${this.playerToDeal}`;
                    this.refreshGame();
                },
                error: (e) => {
                    this.actionResult = e.error;
                }
            });
    }
}
