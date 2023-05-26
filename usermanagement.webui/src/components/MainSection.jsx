import { Container, Card, Button } from 'react-bootstrap';

const MainSection = () => {
    return (
        <div className=' py-5'>
            <Container className='d-flex justify-content-center'>
                <Card className='p-5 d-flex flex-column align-items-center hero-card bg-light w-75'>
                    <h1 className='text-center mb-4'>User Mangement App</h1>
                    <p className='text-center mb-4'>
                        Lorem ipsum dolor sit amet, consectetur adipiscing elit.
                        Phasellus tincidunt tellus a enim euismod, vitae lobortis diam
                        pulvinar. Nulla in ex sed dui pulvinar eleifend nec sit amet tellus. Nam accumsan molestie diam vel dignissim. Proin non interdum urna. Praesent ut mauris luctus, laoreet nulla ac, aliquam diam. Suspendisse orci diam, varius in fringilla et, auctor eu tortor. Morbi semper facilisis arcu, sit amet venenatis odio elementum in. Quisque placerat purus in purus pulvinar lacinia. Sed vulputate eget tortor in pellentesque.
                    </p>
                    <div className='d-flex'>
                        <Button variant='primary' href='/login' className='me-3'>
                            Sign In
                        </Button>
                        <Button variant='secondary' href='/register'>
                            Register
                        </Button>
                    </div>
                </Card>
            </Container>
        </div>
    );
};

export { MainSection };