import { FC, ReactElement, useRef, useEffect, useState } from 'react';
import { CreateCardDto, Client, CardLookupDto } from '../api/api';
import { FormControl } from 'react-bootstrap';
import Card from '@mui/material/Card';
import Box from '@mui/material/Box';
import CardActions from '@mui/material/CardActions';
import CardContent from '@mui/material/CardContent';
import Button from '@mui/material/Button';
import * as React from 'react';
import Typography from '@mui/material/Typography';

const apiClient = new Client('https://localhost:7112/')

async function createCard(card: CreateCardDto) {
    await apiClient.create(card);
    console.log("Card is created");
}

const CardList: FC<{}> = (): ReactElement =>{
    let textInput = useRef(null);
    let [cards, setCards] = useState<CardLookupDto[] | undefined>(undefined);
    
    async function getCards() {
        const cardListVm = await apiClient.getAll();
        setCards(cardListVm.cards);
    }

    useEffect(() => {
        setTimeout(getCards, 500);
    }, []);

    const handleKeyPress = (event: React.KeyboardEvent<HTMLInputElement>) => {
        if(event.key == 'Enter') {
            const card: CreateCardDto = {
                title: event.currentTarget.value,
            };
            createCard(card);
            event.currentTarget.value = '';
            setTimeout(getCards, 500);
        }
    };
    const ChildComponent = ({data} : {data: any}) => {
    return  (
        <React.Fragment>
             <Card sx={{ minWidth: 275 }}>
          <CardContent>
          <div contentEditable="true" onBlur = {async (e) => { await updateCard(data, e); }}>
            {data.title}
            </div>
            <p>
            {data.id}
            </p>
          </CardContent>
          <CardActions>
      <Button size="small" onClick={async () => {await deleteCard(data.id);} }>Delete</Button>
    </CardActions>
    </Card>
        </React.Fragment>
      );
    }

    async function deleteCard(id: any) {
    
        await apiClient.delete(id)
        setCards(cards?.filter((e) => e.id !== id))
    }
    async function updateCard(data: any, e: any){
        data.title = e.target.textContent;
        await apiClient.update(data)
    }
    return (
        <div >
            Cards
            <div className='cont'>
                <FormControl ref={textInput} onKeyPress={handleKeyPress} />
                <div className='flex'>
                {
                    cards?.map((elem) => {
                        return <>
                        <ChildComponent data={elem} />
                      </>
                    })
                }
                </div>
            </div>
            <section>
            </section>
        </div>
    );
};

export default CardList;

