import { TptPoly_BaseClassNonAbstract } from './tpt-poly-base-class-non-abstract.entity';
import { Entity, PrimaryGeneratedColumn, Column, OneToMany } from 'typeorm';

@Entity('tpt_poly_second_level')
export class TptPoly_SecondLevel {
  @PrimaryGeneratedColumn('uuid')
  id: string;

  @Column()
  secondField: string;

  @OneToMany(() => TptPoly_BaseClassNonAbstract, (poly_BaseClassNonAbstracts) => poly_BaseClassNonAbstracts.poly_SecondLevel, { cascade: ['insert', 'update'] })
  poly_BaseClassNonAbstracts: TptPoly_BaseClassNonAbstract[];

  @Column({ nullable: true })
  createdBy?: string;

  @Column({ nullable: true })
  createdDate?: Date;

  @Column({ nullable: true })
  lastModifiedBy?: string;

  @Column({ nullable: true })
  lastModifiedDate?: Date;
}