import { FC, useEffect } from 'react';
import { signoutRedirectCallback } from './user-service';
import { useNavigate } from 'react-router-dom';



const SignoutOidc: FC<{}> = () => {
    const navigate = useNavigate();
    useEffect(() => {
        const signoutAsync = async () => {
            await signoutRedirectCallback();
            navigate('/');
        };
        signoutAsync();
    }, []);
    return <div>Redirecting...</div>;
};

export default SignoutOidc;