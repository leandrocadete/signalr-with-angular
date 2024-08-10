# Create Module

## generate module
`ng g module`
## generate component inside a module
`ng g c modules/login --m login`

## service
`ng g service serviceName`

### Generates a module with a component and add a router in lazy load app-routing.module
```
ng g m modules/home --module app --routing true --route home
```
### Generate Module and Routing Module
```
ng g m modules/acoes --routing true --route acoes

```
### Generate Component inside a Module
```
ng g c acoes-pm --module modules/acoes 
```

















# SignalRTest

This project was generated with [Angular CLI](https://github.com/angular/angular-cli) version 14.2.2.

## Development server

Run `ng serve` for a dev server. Navigate to `http://localhost:4200/`. The application will automatically reload if you change any of the source files.

## Code scaffolding

Run `ng generate component component-name` to generate a new component. You can also use `ng generate directive|pipe|service|class|guard|interface|enum|module`.

## Build

Run `ng build` to build the project. The build artifacts will be stored in the `dist/` directory.

## Running unit tests

Run `ng test` to execute the unit tests via [Karma](https://karma-runner.github.io).

## Running end-to-end tests

Run `ng e2e` to execute the end-to-end tests via a platform of your choice. To use this command, you need to first add a package that implements end-to-end testing capabilities.

## Further help

To get more help on the Angular CLI use `ng help` or go check out the [Angular CLI Overview and Command Reference](https://angular.io/cli) page.

## Project Informations

To run with https
`npm install browser-sync --save-dev`
`ng serve --ssl true --ssl-key ./node_modules/browser-sync/certs/server.key --ssl-cert ./node_modules/browser-sync/certs/server.crt`




