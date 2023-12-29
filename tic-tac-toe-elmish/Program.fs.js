import { toString, Record, Union } from "./fable_modules/fable-library.4.9.0/Types.js";
import { record_type, int32_type, bool_type, list_type, array_type, union_type } from "./fable_modules/fable-library.4.9.0/Reflection.js";
import { copy, fill } from "./fable_modules/fable-library.4.9.0/Array.js";
import { mapIndexed, item as item_1, cons, getSlice, head, tail, isEmpty, filter, ofArray, length, singleton } from "./fable_modules/fable-library.4.9.0/List.js";
import { printf, toText } from "./fable_modules/fable-library.4.9.0/String.js";
import { createElement } from "react";
import { createObj } from "./fable_modules/fable-library.4.9.0/Util.js";
import { Interop_reactApi } from "./fable_modules/Feliz.2.7.0/./Interop.fs.js";
import { ProgramModule_mkSimple, ProgramModule_run } from "./fable_modules/Fable.Elmish.4.0.0/program.fs.js";
import { Program_withReactBatched } from "./fable_modules/Fable.Elmish.React.4.0.0/react.fs.js";
import "./styles.css";


export class Square extends Union {
    constructor(tag, fields) {
        super();
        this.tag = tag;
        this.fields = fields;
    }
    cases() {
        return ["X", "O", "Empty"];
    }
}

export function Square_$reflection() {
    return union_type("Program.Square", [], Square, () => [[], [], []]);
}

export class State extends Record {
    constructor(History, XIsNext, CurrentMove) {
        super();
        this.History = History;
        this.XIsNext = XIsNext;
        this.CurrentMove = (CurrentMove | 0);
    }
}

export function State_$reflection() {
    return record_type("Program.State", [], State, () => [["History", list_type(array_type(Square_$reflection()))], ["XIsNext", bool_type], ["CurrentMove", int32_type]]);
}

export class Msg extends Union {
    constructor(tag, fields) {
        super();
        this.tag = tag;
        this.fields = fields;
    }
    cases() {
        return ["Play", "JumpToMove"];
    }
}

export function Msg_$reflection() {
    return union_type("Program.Msg", [], Msg, () => [[["position", int32_type]], [["move", int32_type]]]);
}

export function init() {
    return new State(singleton(fill(new Array(9), 0, 9, new Square(2, []))), true, 0);
}

export function slice(history, currentMove) {
    const endIndex = (length(history) - 1) | 0;
    const startIndex = (endIndex - currentMove) | 0;
    return [startIndex, endIndex];
}

export function calculateWiner(squares) {
    const lines = ofArray([[0, 1, 2], [3, 4, 5], [6, 7, 8], [0, 3, 6], [1, 4, 7], [2, 5, 8], [0, 4, 8], [2, 4, 6]]);
    const winLine = filter((tupledArg) => {
        const a = tupledArg[0] | 0;
        const b = tupledArg[1] | 0;
        const c = tupledArg[2] | 0;
        const matchValue = squares[a];
        const matchValue_1 = squares[b];
        const matchValue_2 = squares[c];
        let matchResult;
        switch (matchValue.tag) {
            case 0: {
                if (matchValue_1.tag === 0) {
                    if (matchValue_2.tag === 0) {
                        matchResult = 0;
                    }
                    else {
                        matchResult = 2;
                    }
                }
                else {
                    matchResult = 2;
                }
                break;
            }
            case 1: {
                if (matchValue_1.tag === 1) {
                    if (matchValue_2.tag === 1) {
                        matchResult = 1;
                    }
                    else {
                        matchResult = 2;
                    }
                }
                else {
                    matchResult = 2;
                }
                break;
            }
            default:
                matchResult = 2;
        }
        switch (matchResult) {
            case 0:
                return true;
            case 1:
                return true;
            default:
                return false;
        }
    }, lines);
    if (!isEmpty(winLine)) {
        const xs = tail(winLine);
        const a_1 = head(winLine)[0] | 0;
        return squares[a_1];
    }
    else {
        return void 0;
    }
}

export function update(msg, state) {
    if (msg.tag === 1) {
        const m = msg.fields[0] | 0;
        return new State(state.History, state.XIsNext, m);
    }
    else {
        const p = msg.fields[0] | 0;
        const patternInput = slice(state.History, state.CurrentMove);
        const s = patternInput[0] | 0;
        const e = patternInput[1] | 0;
        const history = getSlice(s, e, state.History);
        const currentSquares = head(history);
        const winer = calculateWiner(currentSquares);
        if (winer == null) {
            const next = copy(currentSquares);
            if (next[p].tag === 2) {
                next[p] = (state.XIsNext ? (new Square(0, [])) : (new Square(1, [])));
                return new State(cons(next, history), (length(history) % 2) === 0, length(history));
            }
            else {
                return state;
            }
        }
        else {
            return state;
        }
    }
}

export function render(state, dispatch) {
    let elems_6, elems_4, elems_1, elems_2, elems_3, elems_5;
    let currentSquares;
    const at = slice(state.History, state.CurrentMove)[0] | 0;
    currentSquares = item_1(at, state.History);
    const squares = (n) => {
        const get$ = (index) => currentSquares[index];
        const matchValue = get$(n);
        switch (matchValue.tag) {
            case 1:
                return toString(new Square(1, []));
            case 2:
                return "";
            default:
                return toString(new Square(0, []));
        }
    };
    let status;
    const winer = calculateWiner(currentSquares);
    if (winer == null) {
        const m = state.XIsNext ? (new Square(0, [])) : (new Square(1, []));
        status = toText(printf("Next player: %O"))(m);
    }
    else {
        const w = winer;
        status = toText(printf("Winer: %O"))(w);
    }
    const item = (i) => {
        let elems;
        const desc = (i > 0) ? toText(printf("Go to move %d"))(i) : "Got to game start";
        return createElement("li", createObj(ofArray([["key", i], (elems = [createElement("button", {
            children: desc,
            onClick: (_arg) => {
                dispatch(new Msg(1, [i]));
            },
        })], ["children", Interop_reactApi.Children.toArray(Array.from(elems))])])));
    };
    const moves = mapIndexed((i_1, _arg_1) => item(i_1), state.History);
    const square = (n_1) => createElement("button", {
        className: "square",
        children: squares(n_1),
        onClick: (_arg_2) => {
            dispatch(new Msg(0, [n_1]));
        },
    });
    return createElement("div", createObj(ofArray([["className", "game"], (elems_6 = [createElement("div", createObj(ofArray([["className", "game-board"], (elems_4 = [createElement("div", {
        className: "status",
        children: status,
    }), createElement("div", createObj(ofArray([["className", "board-row"], (elems_1 = [square(0), square(1), square(2)], ["children", Interop_reactApi.Children.toArray(Array.from(elems_1))])]))), createElement("div", createObj(ofArray([["className", "board-row"], (elems_2 = [square(3), square(4), square(5)], ["children", Interop_reactApi.Children.toArray(Array.from(elems_2))])]))), createElement("div", createObj(ofArray([["className", "board-row"], (elems_3 = [square(6), square(7), square(8)], ["children", Interop_reactApi.Children.toArray(Array.from(elems_3))])])))], ["children", Interop_reactApi.Children.toArray(Array.from(elems_4))])]))), createElement("div", createObj(ofArray([["className", "game-info"], (elems_5 = [createElement("ol", {
        children: Interop_reactApi.Children.toArray(Array.from(moves)),
    })], ["children", Interop_reactApi.Children.toArray(Array.from(elems_5))])])))], ["children", Interop_reactApi.Children.toArray(Array.from(elems_6))])])));
}

ProgramModule_run(Program_withReactBatched("root", ProgramModule_mkSimple(init, update, render)));

//# sourceMappingURL=Program.fs.js.map
