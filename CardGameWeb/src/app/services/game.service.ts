import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

const baseUrl = 'https://localhost:7110/Game/';

@Injectable({
    providedIn: 'root'
})
export class GameService {

    constructor(private http: HttpClient) { }

    start(): Observable<any> {
        return this.http.post(`${baseUrl}start`, null);
    }

    getGame(gameId?:number): Observable<any> {
        return this.http.get(`${baseUrl}game?gameId=${gameId}`);
    }

    endGame(gameId?:number): Observable<any> {
        return this.http.delete(`${baseUrl}end?gameId=${gameId}`);
    }

    addDeck(gameId?:number): Observable<any> {
        return this.http.post(`${baseUrl}add-deck?gameId=${gameId}`, null);
    }

    addPlayer(gameId?:number, playerName?:string): Observable<any> {
        return this.http.post(`${baseUrl}add-player?gameId=${gameId}&name=${playerName}`, null);
    }

    removePlayer(gameId?:number, playerName?:string): Observable<any> {
        return this.http.delete(`${baseUrl}remove-player?gameId=${gameId}&name=${playerName}`);
    }

    getPlayers(gameId?:number): Observable<any> {
        return this.http.get(`${baseUrl}players?gameId=${gameId}`);
    }

    deal(gameId?:number, playerName?:string, numberOfCards?:number): Observable<any> {
        return this.http.put(`${baseUrl}deal?gameId=${gameId}&playerName=${playerName}&numberOfCards=${numberOfCards}`, null);
    }

    getUndealt(gameId?:number): Observable<any> {
        return this.http.get(`${baseUrl}undealt?gameId=${gameId}`);
    }

    getUndealtBySuit(gameId?:number): Observable<any> {
        return this.http.get(`${baseUrl}undealt-by-suit?gameId=${gameId}`);
    }

    shuffle(gameId?:number): Observable<any> {
        return this.http.put(`${baseUrl}shuffle?gameId=${gameId}`, null);
    }
}
