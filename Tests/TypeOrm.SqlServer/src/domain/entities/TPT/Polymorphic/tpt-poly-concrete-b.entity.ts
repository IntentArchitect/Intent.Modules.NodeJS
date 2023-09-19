import { TptPoly_BaseClassNonAbstract } from './tpt-poly-base-class-non-abstract.entity';
import { Entity, Column } from 'typeorm';

@Entity('tpt_poly_concrete_b')
export class TptPoly_ConcreteB extends TptPoly_BaseClassNonAbstract {
  @Column()
  concreteField: string;

}